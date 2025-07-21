using Godot;
using System;

public partial class Sword : MeleeBase
{
	public override void _Ready()
	{
		rotationSpeedFormula = () => rotationSpeed + 200f;
		damageFormula = () => damage + 30;

		base._Ready();
	}
}
