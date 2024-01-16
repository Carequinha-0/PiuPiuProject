using Godot;
using System;

public partial class MainMenu : Node2D
{
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		audioStreamPlayer = GetNode<AudioStreamPlayer>("./AudioStreamPlayer");
	}

	public AudioStreamPlayer audioStreamPlayer;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void _on_play_pressed(){
		GetTree().ChangeSceneToFile("res://main.tscn");
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




