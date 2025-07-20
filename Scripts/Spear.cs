using Godot;
using System;

public partial class Spear : MeleeBase
{
	public override void _Ready()
	{
		rotationSpeedFormula = () => (level >= maxLevel) ? 5000f : rotationSpeed + 50;
		damageFormula = () => damage + 30;

		base._Ready();
	}
}
