using Godot;
using System;

public partial class Sword : WeaponBase
{
	private Timer levelTimer;

	public override void _Ready()
	{
		base._Ready();

		levelTimer = GetNode<Timer>("LevelTimer");
		levelTimer.Timeout += () =>
		{
			level += 1;
			rotationSpeed += 30;
			damage += 2f * Mathf.Sqrt(level);

			if (level >= maxLevel) levelTimer.Stop();
		};

		((Area2D)rotator).BodyEntered += (Node2D body) =>
		{
			if (body == this) return;

			if (body.HasMethod("TakeDamage")) body.Call("TakeDamage", damage);
		};
	}
}
