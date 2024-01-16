using Godot;
using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

public partial class RoundTimer : Label
{
	public Door door;
	public override void _Ready()
	{
		this.door = GetNode<Door>("../../../Door");
	}

	public override void _Process(double delta)
	{
		if (door.roundTimer >= 0) {
			this.Text = Math.Round(door.roundTimer).ToString();
		} else {
			this.Text = Math.Abs(Math.Round(door.roundTimer)).ToString();
		}		
	}
	
}
