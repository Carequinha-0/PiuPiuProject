using Godot;
using System;

public partial class CloseRangeMob : CharacterBody2D
{
	
	public const float Speed = 70.0f;
	Vector2 mobPosition = Vector2.Zero;
	Vector2 targetPosition = Vector2.Zero;
	Vector2 playerPosition;
	public Health health;
	DamageReceiver damageReceiver;
	public override void _Ready()
	{
		this.health = new Health(60, onDeath);
		Area2D hitbox = GetNode<Area2D>("./Hitbox");
		this.damageReceiver = new DamageReceiver(ref health, ref hitbox, 0.5f);

	}
	public override void _PhysicsProcess(double delta)
	{
		//Vetores posição
		Vector2 velocity = Velocity; 
		Vector2 mobPosition = this.Position;		
		Vector2 playerPosition = GetNode<CharacterBody2D>("../Player").Position;
		targetPosition = (playerPosition - mobPosition).Normalized();
		velocity = Vector2.Zero;
		if (mobPosition.DistanceTo(playerPosition) < 10000) {
			velocity = targetPosition;
		}
		velocity = velocity * Speed;
		Velocity = velocity;
		MoveAndSlide();
	}
	public override void _Process(double delta)
	{
		damageReceiver.ApplyCollidingDamage((float) delta);
	}

	public void onDeath() {
		this.QueueFree(); // Deletes the Node
	}
	
}

