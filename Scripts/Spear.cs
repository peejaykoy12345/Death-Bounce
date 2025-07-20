using Godot;
using System;

public partial class Spear : MeleeBase
{
	public override void _Ready()
	{
		rotationSpeedFormula = () => rotationSpeed;
		damageFormula = () => damage + 30;

		base._Ready();
	}
}
