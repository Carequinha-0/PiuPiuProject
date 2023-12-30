using Godot;
using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

public partial class HealthBar : ProgressBar
{
	player playerNode;
	public override void _Ready()
	{
		this.playerNode = this.GetNode<player>("../../../Player");
	}

	public override void _Process(double delta)
	{		
		SetValueNoSignal(playerNode.health.current_health);
	}
	
}