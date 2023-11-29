using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class player : CharacterBody2D
{
	public float SPEED = 200f;
	public Vector2 player_movement_velocity = Vector2.Zero;

	public enum PlayerStates {
		Idle,
		WalkingUp,
		WalkingDown,
		WalkingLeft,
		WalkingRight,
	}

	public PlayerStates state = PlayerStates.Idle;

	public StateMachine stateMachine;
    public override void _Ready()
    {
		var defaultstate = new PlayerIdleState(this);
		stateMachine = new StateMachine(defaultstate);
        base._Ready();
    }
    public override void _Process(double delta)
	{
		stateMachine.Input_Process();
		stateMachine.Process(delta);
		//player_movement_velocity = GetMovementVector();
		//if (player_movement_velocity == Vector2.Zero) EnterState(PlayerStates.Idle);
		//else HandleWalkingStates(player_movement_velocity);

	}

	public override void _PhysicsProcess(double delta)
	{
		
		stateMachine.Physics_Process(delta);
		Vector2 final_velocity = Vector2.Zero;
		final_velocity += player_movement_velocity;

		Velocity = final_velocity;

		MoveAndSlide();
	}



	public void HandleWalkingStates (Vector2 player_movement_velocity) {
		const float UP_ANGLE = (float) Math.PI/2;
		const float LEFT_ANGLE = (float) Math.PI;
		const float DOWN_ANGLE = (float) (Math.PI + Math.PI/2);
		const float RIGHT_ANGLE = (float) Math.PI * 2;

		float movement_velocity_angle = player_movement_velocity.Angle();
		GD.Print(movement_velocity_angle);
		if (movement_velocity_angle < (float) UP_ANGLE) EnterState(PlayerStates.WalkingRight);
		else if (movement_velocity_angle < (float) LEFT_ANGLE) EnterState(PlayerStates.WalkingUp);
		else if (movement_velocity_angle < (float) DOWN_ANGLE) EnterState(PlayerStates.WalkingLeft);
		else if (movement_velocity_angle < (float) RIGHT_ANGLE) EnterState(PlayerStates.WalkingDown);
	}

	public void EnterState(PlayerStates new_state) {
		PlayerStates previous_state = this.state;
		this.state = new_state;

		switch (this.state)
		{
			case PlayerStates.Idle:
				EnterIdle();
				break;
			case PlayerStates.WalkingUp:
				EnterWalkingUp();
				break;
			case PlayerStates.WalkingDown:
				EnterWalkingDown();
				break;
			case PlayerStates.WalkingLeft:
				EnterWalkingLeft();
				break;
			case PlayerStates.WalkingRight:
				EnterWalkingRight();
				break;
		}
	}

	public void EnterIdle() {
		AnimationPlayer animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationPlayer.Play("idle");
	}
	public void EnterWalkingUp() {
		AnimationPlayer animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationPlayer.Play("idle");
		//GD.Print("UP");
	}
	public void EnterWalkingDown() {
		AnimationPlayer animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationPlayer.Play("idle");
		//GD.Print("DOWN");
	}
	public void EnterWalkingLeft() {
		AnimationPlayer animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationPlayer.Play("idle");
		//GD.Print("LEFT");
	}
	public void EnterWalkingRight() {
		AnimationPlayer animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationPlayer.Play("idle");
		//GD.Print("RIGHT");
	}
}
