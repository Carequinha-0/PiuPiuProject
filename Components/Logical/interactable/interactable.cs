using System;
using Godot;

public abstract partial class Interactable : Node2D  {
    public float range;
    public string label;
	public Label interaction_text;
	public player player_node;
    public abstract Action OnActivate();
    public abstract void ActionLoop();

    public Action ToggleVisibilityOnRange() {
        interaction_text.Visible = false;
		if (IsOnRange()) {
			interaction_text.Visible = true;
		}
        return null;
    }

    public bool IsOnRange() {
        return player_node.Position.DistanceTo(this.Position) < range;
    }

    public override void _Process(double delta)
	{
		if(IsOnRange() && Input.IsActionJustPressed("interact")){
            this.OnActivate();
        }
        ActionLoop();
	}
}