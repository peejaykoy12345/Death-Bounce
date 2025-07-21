using Godot;
using System;

public partial class Arena : Node2D
{

	private Button RestartButton;

	private AudioStreamPlayer2D ParrySFX;

	private bool win_claimed = false;
	public override void _Ready()
	{
		ParrySFX = GetNode<AudioStreamPlayer2D>("ParrySFX");
		RestartButton = GetNode<Button>("RestartButton");
		RestartButton.Visible = false;

		RestartButton.Pressed += () => GetTree().ReloadCurrentScene();
	}

	public override void _Process(double delta)
	{
		int weapon_count = 0;

		WeaponBase lastWeaponSelected = null;

		foreach (Node child in GetChildren())
		{
			if (child is WeaponBase wb) {
				weapon_count++;
				lastWeaponSelected =  wb;
			};
		}

		if (weapon_count <= 1)
		{
			if (!win_claimed && lastWeaponSelected != null)
			{
				GetNode<WinTracker>("/root/WinTracker").AddWin(lastWeaponSelected.Name, 1);
				win_claimed = true;
			}
			RestartButton.Visible = true;
		}
    }

	public void playParrySound()
	{
		if (ParrySFX.Playing) return;

		ParrySFX.Play();
	}
}
