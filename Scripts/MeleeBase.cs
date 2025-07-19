using Godot;
using System;

public partial class MeleeBase : WeaponBase
{
	protected Timer levelTimer;

	public Func<float> rotationSpeedFormula;
	public Func<float> damageFormula;
	public override void _Ready()
	{
		base._Ready();

		levelTimer = GetNode<Timer>("LevelTimer");
		levelTimer.Timeout += () =>
		{
			level += 1;
			rotationSpeed = rotationSpeedFormula();
			damage = damageFormula();

			if (level >= maxLevel) levelTimer.Stop();
		};

		rotator.BodyEntered += (Node2D body) =>
		{
			if (body == this) return;

			if (body.HasMethod("TakeDamage")) body.Call("TakeDamage", this, damage);
		};
	}
}
