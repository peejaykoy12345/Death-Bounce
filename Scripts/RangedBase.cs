using Godot;
using System;

public partial class RangedBase : WeaponBase
{
    [ExportGroup("Script Settings")]
    [Export] protected PackedScene projectile;
    [Export] protected Marker2D projectile_start_marker;
    [Export] protected Timer shoot_interval;

    // Arena variables
    protected Node2D Projectiles_Node;

    // Stat calculators
    protected Func<float> damageFormula;

    public override void _Ready()
    {
        base._Ready();

        Projectiles_Node = arena.GetNode<Node2D>("Projectiles");

        levelTimer.Timeout += () =>
        {
            damage = damageFormula();
        };
    }

    public void shoot()
    {
        Arrow projectile_instantiated = (Arrow) projectile.Instantiate();
        projectile_instantiated.damage = damage;
        projectile_instantiated.owner = this;
        projectile_instantiated.GlobalPosition = projectile_start_marker.GlobalPosition;
        projectile_instantiated.GlobalRotation = rotator.GlobalRotation + Mathf.DegToRad(90f);
        Projectiles_Node.AddChild(projectile_instantiated);
    }
}