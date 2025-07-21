using Godot;
using System;

public partial class Sword : MeleeBase
{
	public override void _Ready()
	{
		rotationSpeedFormula = () => rotationSpeed + 25 * level;
		damageFormula = () => damage + 30;

		base._Ready();
	}
}
