using Godot;
using System;

public partial class Door : Interactable
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.player_node = GetNode<player>("../Player");
		this.interaction_text = GetNode<Label>("./InteractionText");
		this.range = 100;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.

	public override void ActionLoop() {
		ToggleVisibilityOnRange();
	}

	public override Action OnActivate()
	{
		Random rnd = new Random();
		String[] levels = {"res://Components/levels/level_1.tscn", "res://Components/levels/level_2.tscn", "res://Components/levels/level_3.tscn"};
		GetTree().ChangeSceneToFile(levels[rnd.Next(0,3)]);
		return null;
	}
}
