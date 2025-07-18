using Godot;
using System;

public partial class Sword : CharacterBody2D
{

	Node2D rotator;

	public override void _Ready()
	{
		rotator = GetNode<Node2D>("Rotator");
	}

	public override void _Process(double delta)
	{
		
	}
}
