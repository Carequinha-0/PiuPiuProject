using Godot;
using System;

public partial class player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector2 new_velocity = Velocity;

		if(Input.IsKeyPressed(Key.W)) new_velocity = new Vector2(0,-40);
		if(Input.IsKeyPressed(Key.S)) new_velocity = new Vector2(0, 40);
		if(Input.IsKeyPressed(Key.D)) new_velocity = new Vector2(40, 0);
		if(Input.IsKeyPressed(Key.A)) new_velocity = new Vector2(-40, 0);

		Velocity = new_velocity;
		MoveAndSlide();
	}
}
