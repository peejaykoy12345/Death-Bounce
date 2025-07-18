using Godot;
using System;

public partial class WeaponBase : CharacterBody2D
{
	[ExportGroup("Attack Settings")]
	[Export] public float rotationSpeed = 500f;
	[Export] public float damage = 10f;
	[Export] public float radius = 32f;
	[Export] public int level = 1;

	[ExportGroup("Defense Settings")]
	[Export] public float health = 100f;

	protected Sprite2D sprite;
    protected Node2D rotator;
	protected CollisionShape2D collision;

	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite");
		rotator = GetNode<Area2D>("Rotator");
		collision = GetNode<CollisionShape2D>("CollisionShape2D");

		float scale = radius * 2 / sprite.Texture.GetSize().X;
		sprite.Scale = new Vector2(scale, scale);
		((CircleShape2D)collision.Shape).Radius = radius;

	}

	public override void _Process(double delta)
	{
		rotator.RotationDegrees += rotationSpeed * (float)delta;
	}

	public void TakeDamage(float dmg)
	{
		health = Mathf.Max(0, health - dmg);
		if (health <= 0)
			QueueFree();
	}
}
