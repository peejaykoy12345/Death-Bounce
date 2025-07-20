using Godot;
using System;

public partial class Arena : Node2D
{

	private AudioStreamPlayer2D ParrySFX;
	public override void _Ready()
	{
		ParrySFX = GetNode<AudioStreamPlayer2D>("ParrySFX");
	}
	public void playParrySound()
	{
		if (ParrySFX.Playing) return;

		ParrySFX.Play();
	}
}
