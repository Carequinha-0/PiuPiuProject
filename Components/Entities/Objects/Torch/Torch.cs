using Godot;
using System;

public partial class Torch : InteractableToggleable
{
	// Called when the node enters the scene tree for the first time.
	public PointLight2D light_node;
	public override void _Ready()
	{
		this.player_node = GetNode<player>("../Player");
		this.interaction_text = GetNode<Label>("./InteractionText");
		this.light_node = GetNode<PointLight2D>("./PointLight2D");
		this.range = 100;
		this.active = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.

	public override void ActionLoop(double delta) {
		ToggleVisibilityOnRange();
		if (this.active) {
			this.light_node.Visible = true;
		} else {
			this.light_node.Visible = false;
		}
	}

	public override Action OnActivate()
	{
		this.Toggle();
		return null;
	}
}
