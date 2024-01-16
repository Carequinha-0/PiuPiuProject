using Godot;
using System;

public partial class level_2 : Node2D
{
	public BigTitle bigTitle;
	public Door door;
	public double round_spawn_cooldown = global_state.compute_round_spawn_cooldown();
	public double elapsed_spawn_time = 0;
	public override void _Ready()
	{
		this.bigTitle = GetNode<BigTitle>("./BigTitle");
		this.door = GetNode<Door>("./Door");
		bigTitle.SetText("Floor " + global_state.round);
		bigTitle.ToAnimateOpacity(0, 2);
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		elapsed_spawn_time += delta;
		if(elapsed_spawn_time > round_spawn_cooldown && door.roundTimer > 0) {
			elapsed_spawn_time = 0;

			Random rnd = new Random();

			var shooting_mob = GD.Load<PackedScene>("res://shootingMob.tscn");
			var close_range_mob = GD.Load<PackedScene>("res://CloseRangeMob.tscn");
			PackedScene[] mobLists = {shooting_mob, close_range_mob};
			var enemy1 = mobLists[rnd.Next(0,2)].Instantiate<CharacterBody2D>();
			AddChild(enemy1);

			var nodes = GetTree().GetNodesInGroup("Markers");
			var selectedNode = nodes[(int) rnd.Next(0, nodes.Count + 1)] as Marker2D;
			var position = selectedNode.Position;
			enemy1.Position = position;
		}
	}
}
