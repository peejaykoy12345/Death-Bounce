using Godot;
using System;

public partial class Bow : RangedBase
{
	public override void _Ready()
	{
		damageFormula = () => damage * 1.5f;

		base._Ready();

		shoot_interval.Timeout += () => shoot();
	}
}
