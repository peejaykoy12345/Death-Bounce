using Godot;
using System;

public partial class Spear : MeleeBase
{
	public override void _Ready()
	{
		rotationSpeedFormula = () => rotationSpeed;
		damageFormula = () => 2f * Mathf.Sqrt(level);

		base._Ready();
	}
}
