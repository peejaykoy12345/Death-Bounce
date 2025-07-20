using Godot;
using System;

public partial class Sword : MeleeBase
{
	public override void _Ready()
	{
		rotationSpeedFormula = () => rotationSpeed + 100f;
		damageFormula = () => damage + 50;

		base._Ready();
	}
}
