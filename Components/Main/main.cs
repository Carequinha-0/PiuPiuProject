using Godot;
using System;

public partial class Main : Node2D {

	public override void _Ready()
	{
		
	}

	public override void _Process(double delta)
	{
		var scene = GD.Load<PackedScene>("CloseRangeMob");
	    var instance = scene.Instantiate<CharacterBody2D>();
        AddChild(instance);
        instance.Position = new Vector2(0,0);
	}

    private void _on_timer_timeout()
    {

    }
}