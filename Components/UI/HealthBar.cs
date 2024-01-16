using Godot;
using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

public partial class HealthBar : AnimatedSprite2D
{
	player playerNode;
	public override void _Ready()
	{
		this.playerNode = this.GetNode<player>("../../../Player");
	}

	public override void _Process(double delta)
	{		
		var health_percentage = this.playerNode.health.GetHealthPercentage();
		if (health_percentage == 100) {
			this.Frame = 0;
		} else if (health_percentage > 90) {
			this.Frame = 1;
		} else if (health_percentage > 80) {
			this.Frame = 2;
		} else if (health_percentage > 70) {
			this.Frame = 3;
		} else if (health_percentage > 60) {
			this.Frame = 4;
		} else if (health_percentage > 50) {
			this.Frame = 5;
		} else if (health_percentage > 40) {
			this.Frame = 6;
		} else if (health_percentage > 30) {
			this.Frame = 7;
		} else if (health_percentage > 20) {
			this.Frame = 8;
		} else if (health_percentage > 10) {
			this.Frame = 9;
		}
	}
	
}
