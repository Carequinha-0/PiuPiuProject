using Godot;
using System;

public partial class Main : Node2D
{
	public Node2D SpawnArea;
	public double spawnCooldown;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		spawnCooldown += delta;
		if(Input.IsActionJustPressed("Esc")) {
			GetTree().ChangeSceneToFile("res://MainMenu.tscn");
		}
		if (spawnCooldown > 2) {
			Random rnd = new Random();

			var shooting_mob = GD.Load<PackedScene>("res://shootingMob.tscn");
			var close_range_mob = GD.Load<PackedScene>("res://CloseRangeMob.tscn");
			PackedScene[] mobLists = {shooting_mob, close_range_mob};
			var enemy1 = mobLists[rnd.Next(0,2)].Instantiate<CharacterBody2D>();
			AddChild(enemy1);

			SpawnArea = GetNode<Node2D>("SpawnLocation");
			var nodes = GetTree().GetNodesInGroup("Markers");
			var selectedNode = nodes[(int) rnd.Next(0, nodes.Count + 1)] as Marker2D;
			var position = selectedNode.Position;
			enemy1.Position = position;

			spawnCooldown = 0;
		}
	}
	
} 
