using Godot;
using System;

public partial class Bow : RangedBase
{

	private int shoot_per_interval = 1;
	public override void _Ready()
	{
		damageFormula = () => damage * 1.5f;

		base._Ready();

		levelTimer.Timeout += () => shoot_per_interval++;

		shoot_interval.Timeout += () => bow_shoot();
	}

	public async void bow_shoot()
	{
		for (int i = 0; i < shoot_per_interval; i++)
		{
			await ToSignal(GetTree().CreateTimer(0.1f), "timeout");

			shoot();
		}
	}
}
