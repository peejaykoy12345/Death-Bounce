using Godot;
using System;

public partial class Arena : Node2D
{

	private Button RestartButton;

	private AudioStreamPlayer2D ParrySFX;
	public override void _Ready()
	{
		ParrySFX = GetNode<AudioStreamPlayer2D>("ParrySFX");
		RestartButton = GetNode<Button>("RestartButton");
		RestartButton.Visible = false;

		RestartButton.Pressed += () =>GetTree().ReloadCurrentScene();
	}

	public override void _Process(double delta)
	{
		int weapon_count = 0;

		foreach (Node child in GetChildren())
		{
			if (child is WeaponBase) weapon_count++;
		}

		if (weapon_count <= 1)
		{
			RestartButton.Visible = true;
		}
    }

	public void playParrySound()
	{
		if (ParrySFX.Playing) return;

		ParrySFX.Play();
	}
}
