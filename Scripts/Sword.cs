using Godot;
using System;

public partial class Sword : CharacterBody2D
{
	[ExportGroup("Attack Settings")]
	[Export]
	public float rotationSpeed = 500f;
	[Export]
	public float damage = 10f;
	public int level = 1;

	[ExportGroup("Defense Settings")]
	[Export]
	public float health = 100f;

	private Area2D rotator;

	private Timer level_timer;


	public override void _Ready()
	{
		rotator = GetNode<Area2D>("Rotator");
		level_timer = GetNode<Timer>("LevelTimer");

		level_timer.Timeout += () =>
		{
			level += 1;

			rotationSpeed *= 1 + 0.1f * (Mathf.Log(1 + level) / Mathf.Log(2));
			damage += 2f * Mathf.Sqrt(level);
		};

		rotator.BodyEntered += (Node2D body) =>
		{
			if (body == this)
			{
				return;
			}
			if (body.HasMethod("TakeDamage"))
			{
				body.Call("TakeDamage", damage);
			}
		};
	}

	public override void _Process(double delta)
	{
		rotator.RotationDegrees += rotationSpeed * (float)delta;
	}

	public void TakeDamage(float damage_taken)
	{
		health = Mathf.Max(0, health - damage_taken);
		if (health <= 0)
		{
			QueueFree();
		}
	}
}
