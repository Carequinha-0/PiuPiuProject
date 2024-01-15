using Godot;
using System;

public partial class deathScreen : Node2D
{
	private void _on_restart_game_pressed()
	{
		this.GetTree().ChangeSceneToFile("res://Components/levels/level_1.tscn");
	}
	private void _on_button_pressed()
	{
		this.GetTree().Quit();
	}
}






