using Godot;
using System;

public partial class global_state : Node
{
	public static int round = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public static int compute_round_spawn_cooldown()
	{
		if (round < 5) {
			return 3;
		} else if (round <= 10) {
			return 2;
		} else if (round < 15) {
			return 1;
		} else {
			return 0;
		}
	}
}
