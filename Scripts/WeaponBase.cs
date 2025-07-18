using Godot;
using System;

public partial class WeaponBase : CharacterBody2D
{
	[ExportGroup("Attack Settings")]
	[Export] public float rotationSpeed = 500f;
	[Export] public float damage = 10f;
	[Export] public float radius = 32f;
	[Export] public int level = 1;

	[ExportGroup("Defense Settings")]
	[Export] public float health = 100f;

    [ExportGroup("Movement Settings")]
    [Export] public float speed = 100f;
    
    protected Node2D rotator;

	public override void _Ready()
	{
		rotator = GetNode<Area2D>("Rotator");
	}

	public override void _Process(double delta)
	{
		rotator.RotationDegrees += rotationSpeed * (float)delta;
	}

	public void TakeDamage(float dmg)
	{
		health = Mathf.Max(0, health - dmg);
		if (health <= 0)
			QueueFree();
	}
}
