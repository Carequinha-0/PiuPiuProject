using Godot;
using System;

public partial class ShootingMobBullet : DamageSource
{
	public Vector2 direction;
	public int velocity;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.damage = 10;
		this.velocity = 6;
		var anim_player = this.GetNode<AnimationPlayer>("./AnimationPlayer");
		anim_player.CurrentAnimation = "default";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.Position += direction*velocity;
	}

	public override void OnDamageDealt()
	{
		this.QueueFree();
	}
}