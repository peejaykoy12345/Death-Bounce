using Godot;
using System;

public partial class Arrow : Area2D
{
    [Export] public float damage = 50;

    [ExportGroup("Script Settings")]
    [Export] public VisibleOnScreenNotifier2D VisibleOnScreenNotifier;

    public CharacterBody2D owner;

    private const float speed = 500.0f;

    public override void _Ready()
    {

        VisibleOnScreenNotifier.ScreenExited += () => QueueFree();

        BodyEntered += (Node2D body) =>
        {
            if (body != owner && body is WeaponBase wb)
            {
                wb.TakeDamage(owner, damage);
            }
        };
    }

    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += -(Transform.Y * speed * (float)delta);
    }
}