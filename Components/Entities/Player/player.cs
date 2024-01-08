using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class player : CharacterBody2D
{
	public StateMachine stateMachine;
	public float SPEED = 150f;
	public Vector2 player_movement_velocity = Vector2.Zero;
	public Health health;
	public DamageReceiver damage_receiver { get; set; }

	public float dashCooldown = 1;
	public float dashCurrentCooldown = 0;
	public float normalShotCooldown = 0.1f;
	public float normalShotCurrentCooldown = 0;


	public override void _Ready()
	{
		this.health = new Health(100, OnDeath);
		Area2D hitbox = GetNode<Area2D>("./Hitbox");
		this.damage_receiver = new DamageReceiver(ref health, ref hitbox, 0.1f);
		var defaultstate = new PlayerIdleState(this);
		stateMachine = new StateMachine(defaultstate);
		base._Ready();
	}
	public override void _Process(double delta)
	{
		stateMachine.Input_Process();
		stateMachine.Process(delta);
		damage_receiver.ApplyCollidingDamage((float)delta);
	}

	public override void _PhysicsProcess(double delta)
	{
		
		stateMachine.Physics_Process(delta);
		Vector2 final_velocity = Vector2.Zero;
		final_velocity += player_movement_velocity;

	
		dashCurrentCooldown -= (float)delta;
		if (dashCurrentCooldown < 0) {
			dashCurrentCooldown=0;
		}		
		normalShotCurrentCooldown -= (float)delta;
		if (normalShotCurrentCooldown < 0) {
			normalShotCurrentCooldown = 0;
		}

		Velocity = final_velocity;

		MoveAndSlide();
	}

	public void OnDeath()
	{
		GD.Print("Player died");
		
	}
}
