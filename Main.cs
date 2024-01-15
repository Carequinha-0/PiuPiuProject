using Godot;
using System;

public partial class Main : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void OnTimerTimeout()
	{
		return; // TEMPORARY MUST REMOVE
		var scene = GD.Load<PackedScene>("res://CloseRangeMob.tscn");
		var enemy1 = scene.Instantiate<CharacterBody2D>();
		AddChild(enemy1);
		enemy1.Position = new Vector2(0,0);
	}
}
