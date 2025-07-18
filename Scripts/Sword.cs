using Godot;
using System;

public partial class Sword : WeaponBase
{
	private Timer levelTimer;

	public override void _Ready()
	{
		base._Ready();

		levelTimer = GetNode<Timer>("LevelTimer");
		levelTimer.Timeout += OnLevelUp;
		levelTimer.Start();

		((Area2D)rotator).BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body == this) return;

		if (body.HasMethod("TakeDamage"))
			body.Call("TakeDamage", damage);
	}

	private void OnLevelUp()
	{
		level += 1;
		rotationSpeed *= 1 + 0.1f * (Mathf.Log(1 + level) / Mathf.Log(2));
		damage += 2f * Mathf.Sqrt(level);
	}
}
