using Godot;
using System;
using System.Collections.Generic;

public partial class Dagger : WeaponBase
{
	private Timer levelTimer;

	// A list for storing every character who is poisoned
	private List<CharacterBody2D> poison_affected_characters = new List<CharacterBody2D>();
	private float poison_damage;

	public override void _Ready()
	{
		base._Ready();

		levelTimer = GetNode<Timer>("LevelTimer");
		levelTimer.Timeout += () =>
		{
			level += 1;
			rotationSpeed += 30;
			damage += 2f * Mathf.Sqrt(level);

			if (level >= maxLevel) levelTimer.Stop();
		};

		poison_damage = damage / 4;

		((Area2D)rotator).BodyEntered += (Node2D body) =>
		{
			if (body == this) return;

			if (body.HasMethod("TakeDamage"))
			{
				body.Call("TakeDamage", damage);
				ApplyPoison((CharacterBody2D)body);
			}
		};
	}

	private async void ApplyPoison(CharacterBody2D character)
	{
		if (poison_affected_characters.Contains(character)) return;

		poison_affected_characters.Add(character);
		_ = RemovePoisonAfterDelay(character); 

		while (poison_affected_characters.Contains(character))
		{
			await ToSignal(GetTree().CreateTimer(1.0), "timeout");

			if (IsInstanceValid(character))
			{
				character.Call("TakeDamage", poison_damage);
			}
		}
	}
	
	private async System.Threading.Tasks.Task RemovePoisonAfterDelay(CharacterBody2D character)
	{
		await ToSignal(GetTree().CreateTimer(5.0), "timeout");
		poison_affected_characters.Remove(character);
	}
}
