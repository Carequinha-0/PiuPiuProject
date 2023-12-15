using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class CloseRangeMob : CharacterBody2D
{
	public const float Speed = 100.0f;

	Vector2 mobPosition = Vector2.Zero;
	Vector2 targetPosition = Vector2.Zero;
	Vector2 playerPosition;
		
	public override void _PhysicsProcess(double delta)
	{
		//Vetores posição
		Vector2 velocity = Velocity; 
		Vector2 mobPosition = this.Position;		
		Vector2 playerPosition = GetNode<CharacterBody2D>("../Player").Position;
		
		targetPosition = (playerPosition - mobPosition).Normalized();
		velocity = Vector2.Zero;
		if (mobPosition.DistanceTo(playerPosition) < 200) {
			velocity = targetPosition;
		}
		velocity = velocity * Speed;
		Velocity = velocity;	
		MoveAndSlide();

	}
	
}

