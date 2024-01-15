using Godot;
using System;

public partial class MainMenu : Node2D
{
	
	public void _on_play_pressed(){
		GetTree().ChangeSceneToFile("res://Components/levels/level_1.tscn");
	}
	public void _on_quit_pressed(){
		GetNode<Window>("Confirmation").Show();
	}
	public void _on_confirmation_close_requested() {
		GetNode<Window>("Confirmation").Hide();
	}
	private void _on_yes_pressed()
	{
		GetTree().Quit();
	}
	private void _on_no_pressed()
	{
		GetNode<Window>("Confirmation").Hide();
	}
}




