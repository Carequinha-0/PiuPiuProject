using Godot;
using System;

public partial class Door : Interactable
{
	public double roundTimer = 60;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.player_node = GetNode<player>("../Player");
		this.interaction_text = GetNode<Label>("./InteractionText");
		this.range = 100;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.

	public override void ActionLoop(double delta) {
		ToggleVisibilityOnRange();
		if (roundTimer >= 0) {
			roundTimer -= delta;
		}
	}

	public override Action OnActivate()
	{
		Random rnd = new Random();
		if (roundTimer <= 0) {
			if (global_state.round < 14) {		
				string[] levels = {"res://Components/levels/level_1.tscn", "res://Components/levels/level_2.tscn", "res://Components/levels/level_3.tscn"};
				global_state.round += 1;
				GetTree().ChangeSceneToFile(levels[rnd.Next(0,3)]);
			} else {
				GetTree().ChangeSceneToFile("res://Components/levels/level_boss.tscn");
			}
		}
		return null;
	}
}
