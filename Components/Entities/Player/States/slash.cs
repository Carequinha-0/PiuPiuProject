using Godot;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public partial class PlayerSlashState : State
{
	public new player parent_node;
	public PlayerSlashState(player parent_node) : base(parent_node)
	{
		this.parent_node = parent_node;
	}
	public float x_strength;
	public float y_strength;
	Vector2 mousePos = Vector2.Zero;
	Vector2 currentPos = Vector2.Zero;
	Vector2 Vector = Vector2.Zero;
	float angleToAxis;
	double timer;
	override public void _EnterState() {
		parent_node.GetNode<Area2D>("./Weapon/Slash").Visible = true;
		parent_node.GetNode<CollisionShape2D>("./Weapon/Slash/CollisionShape2D").Disabled = false;
		currentPos = parent_node.GetNode<CharacterBody2D>(".").Position;
		mousePos = parent_node.GetGlobalMousePosition();
		Vector = mousePos - currentPos;
		angleToAxis = Vector.Angle();
		parent_node.GetNode<Node2D>("./Weapon").Rotation = angleToAxis;
		timer = 0;
	}

	override public void _ExitState() {
		mousePos = Vector2.Zero;
		currentPos = Vector2.Zero;
		angleToAxis = 0;
		parent_node.GetNode<Area2D>("./Weapon/Slash").Visible = false;
		parent_node.GetNode<CollisionShape2D>("./Weapon/Slash/CollisionShape2D").Disabled = true;
	}
	override public void _InputProcess() {
		x_strength = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
		y_strength = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");
	}
	
	override public State _process_state(double delta) {
		timer += delta;
		if (timer >= 0.5) {
			return new PlayerIdleState(parent_node);
		}
		Vector2 speed_vector = new Vector2
		{
			X = x_strength,
			Y = y_strength
		};		
		speed_vector = speed_vector.Normalized();
		parent_node.player_movement_velocity = speed_vector * parent_node.SPEED;

		AnimationPlayer animationPlayer = this.parent_node.GetNode<AnimationPlayer>("./AnimationPlayer");
		animationPlayer.CurrentAnimation = GetAnimationNameBySpeed(speed_vector);

		if (Input.IsActionJustPressed("dash") && parent_node.dashCurrentCooldown == 0) {
			return new PlayerDashState(parent_node);
		}

		return null;
	}

	override public State _physics_process_state(double delta) {
		return null;
	}

	public string GetAnimationNameBySpeed(Vector2 speed_vector) {
		if(speed_vector.X > 0) return "running_right";
		if(speed_vector.X < 0) return "running_left";
		if(speed_vector.Y < 0) return "running_up";
		if(speed_vector.Y > 0) return "running_down";
		return null;
	}
}
