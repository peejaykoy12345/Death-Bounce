using Godot;
using System;

public partial class Spear : MeleeBase
{
	public override void _Ready()
	{
		rotationSpeedFormula = () => rotationSpeed + 30 * level;

		base._Ready();
	}
}
