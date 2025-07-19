using Godot;
using System;
using System.Threading.Tasks;

public partial class WeaponBase : CharacterBody2D
{
	Random rand = new Random();

	[ExportGroup("Attack Settings")]
	[Export] public float rotationSpeed = 500f;
	[Export] public float damage = 10f;

	[ExportGroup("Defense Settings")]
	[Export] public float health = 100f;

	[ExportGroup("Movement Settings")]
	[Export] public float speed = 100f;

	[ExportGroup("Level Settings")]
	[Export] public int level = 1;
	[Export] public int maxLevel = 10;

	[ExportGroup("Script Settings")]
	[Export] public Camera2D camera;

	public Node2D arena;

	public LineEdit HealthIndicator;

	protected int dx;
	protected int dy;

	protected Area2D rotator;
	protected Timer levelTimer;
	private int rotation_direction = 1;

	protected Vector2 screenSize;

	private float left;
	private float right;
	private float top;
	private float bottom;

	// Status
	protected bool isNotFrozen = true;

	private const float bounce_offset = 40f;

	public override void _Ready()
	{
		arena = (Node2D)GetParent();

		rotator = GetNode<Area2D>("Rotator");
		levelTimer = GetNode<Timer>("LevelTimer");

		HealthIndicator = GetNode<LineEdit>("HealthIndicator");

		HealthIndicator.Text = health.ToString();

		Vector2 camCenter = camera.GetScreenCenterPosition();
		screenSize = camera.GetViewportRect().Size / 2;

		left = camCenter.X - screenSize.X + bounce_offset;
		right = camCenter.X + screenSize.X - bounce_offset;
		top = camCenter.Y - screenSize.Y + bounce_offset;
		bottom = camCenter.Y + screenSize.Y - bounce_offset;

		dx = rand.Next(0, 2) == 0 ? -1 : 1;
		dy = rand.Next(0, 2) == 0 ? -1 : 1;

		rotator.AreaEntered += async (Area2D area) =>
		{
			CharacterBody2D area_parent = (CharacterBody2D)area.GetParent();
			GD.Print($"parent check: {area_parent.Name} method check: {area_parent.HasMethod("onParry")}");
			if (area_parent is WeaponBase wb)
			{
				await wb.onParry();
			}
		};
	}

	public override void _Process(double delta)
	{
		HealthIndicator.Text = health.ToString();

		if (isNotFrozen) rotator.RotationDegrees += rotationSpeed * rotation_direction * (float)delta;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (isNotFrozen)
		{
			Velocity = new Vector2(speed * dx, speed * dy);
			MoveAndSlide();
		}

		int slideCount = GetSlideCollisionCount();
		for (int i = 0; i < slideCount; i++)
		{
			KinematicCollision2D collision = GetSlideCollision(i);
			Node2D collider = (Node2D)collision.GetCollider();

			if (collider == this) continue;

			if (collider is CharacterBody2D victim)
			{
				if (IsInstanceValid(victim) && victim.HasMethod("BounceOffBody")) victim.Call("BounceOffBody");
			}
		}

		if (Position.X <= left || Position.X >= right) dx *= -1;
		if (Position.Y <= top || Position.Y >= bottom) dy *= -1;
	}

	public async Task onParry()
	{
		GD.Print($"Ran from {this.Name}");
		arena.Call("playParrySound");
		rotation_direction *= -1;

		await Freeze(0.2f);
	}

	public void BounceOffBody()
	{
		dx *= -1;
		dy *= -1;
	}

	public async Task Freeze(float time)
	{
		isNotFrozen = false;
		await ToSignal(GetTree().CreateTimer(time), "timeout");

		isNotFrozen = true;
	}

	int count;
	public async void TakeDamage(CharacterBody2D attacker, float dmg)
	{
		health = Mathf.Max(0, health - dmg);
		count++;
		if (health <= 0)
		{
			QueueFree();
			return;
		}

		if (attacker.HasMethod("Freeze")) attacker.Call("Freeze", 0.2f);

		await Freeze(0.2f);
	}

	public void TakeDamageButWithoutStun(float dmg)
	{
		health = Mathf.Max(0, health - dmg);
	}
}
