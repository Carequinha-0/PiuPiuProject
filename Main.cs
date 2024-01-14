using Godot;
using System;

public partial class Main : Node2D
{
	public Area2D SpawnArea;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("Esc")) {
			GetTree().ChangeSceneToFile("res://MainMenu.tscn");
		}
	}
	
	private void _on_timer_timeout()
	{		
		var scene = GD.Load<PackedScene>("res://CloseRangeMob.tscn");
		var enemy1 = scene.Instantiate<CharacterBody2D>();
		AddChild(enemy1);

		Random rnd = new Random();

		SpawnArea = GetNode<Area2D>("./SpawnLocation");
		var nodes = GetTree().GetNodesInGroup("Spawners");
		var selectedNode = nodes[(int) rnd.Next(0, nodes.Count + 1)] as Marker2D;
		var position = selectedNode.Position;
		enemy1.Position = position;		
	}
} 
