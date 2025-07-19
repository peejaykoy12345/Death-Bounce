using Godot;
using System;

public partial class WeaponBase : CharacterBody2D
{
	Random rand = new Random();

	[ExportGroup("Attack Settings")]
	[Export] public float rotationSpeed = 500f;
	[Export] public float damage = 10f;
	[Export] public int level = 1;

	[ExportGroup("Defense Settings")]
	[Export] public float health = 100f;

    [ExportGroup("Movement Settings")]
    [Export] public float speed = 100f;

	[ExportGroup("Script Settings")]
	[Export] public Camera2D camera;

	protected int dx;
	protected int dy;
    
    protected Node2D rotator;

	protected Vector2 screenSize;

	protected float left;
	protected float right;
	protected float top;
	protected float bottom;

	protected const float bounce_offset = 40f;

	public override void _Ready()
	{

		rotator = GetNode<Area2D>("Rotator");

		Vector2 camCenter = camera.GetScreenCenterPosition();
		screenSize = camera.GetViewportRect().Size / 2;

		left = camCenter.X - screenSize.X + bounce_offset;
		right = camCenter.X + screenSize.X - bounce_offset;
		top = camCenter.Y - screenSize.Y + bounce_offset;
		bottom = camCenter.Y + screenSize.Y - bounce_offset;

		dx = rand.Next(0, 2) == 0 ? -1 : 1;
		dy = rand.Next(0, 2) == 0 ? -1 : 1;
	}

	public override void _Process(double delta)
	{
		rotator.RotationDegrees += rotationSpeed * (float)delta;
	}

	public override void _PhysicsProcess(double delta)
	{
		Velocity = new Vector2(speed * dx, speed * dy);
		MoveAndSlide();

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

	public void BounceOffBody()
	{
		dx *= -1;
		dy *= -1;
	}

	int count;
	public void TakeDamage(float dmg)
	{
		health = Mathf.Max(0, health - dmg);
		count++;
		GD.Print($"I got hit from {this.Name}, health: {health}");
		if (health <= 0) QueueFree();
	}
}
