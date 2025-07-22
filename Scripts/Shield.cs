using Godot;
using System;

public partial class Shield : WeaponBase
{
	[ExportGroup("Script Settings")]
	[Export] CollisionShape2D collisionShape;
	private Vector2 size;
	private float scale_multiplier = 2;
	public override void _Ready()
	{
		base._Ready();

		size = weapon_sprite.Scale;

		resize();

		levelTimer.Timeout += () =>
		{
			level += 1;
			scale_multiplier = level + 1;
			resize();
			if (level >= maxLevel) levelTimer.Stop();
		};

		rotator.AreaEntered += async  (Area2D area) =>
		{
			if (area is Arrow arrow)
			{
				await arrow.owner.TakeDamage(this, arrow.damage);
				return;
			}

			CharacterBody2D area_parent = (CharacterBody2D)area.GetParent();
			if (area_parent is WeaponBase wb)
			{
				await wb.TakeDamage(this, wb.damage);
			}
		};
	}

	private void resize()
	{
		/*weapon_sprite.Scale = size * scale_multiplier;

		Vector2 textureSize = weapon_sprite.Texture.GetSize();     
		Vector2 spriteScale = weapon_sprite.Scale;                
		Vector2 finalSize = textureSize * spriteScale;     

		CapsuleShape2D shape = (CapsuleShape2D)collisionShape.Shape; // error here
		shape.Radius = finalSize.X / 2.0f;
		shape.Height = Mathf.Max(1.0f, shape.Height);*/

		weapon_sprite.Scale = size * scale_multiplier;
		collisionShape.Scale = weapon_sprite.Scale;
	}
}
