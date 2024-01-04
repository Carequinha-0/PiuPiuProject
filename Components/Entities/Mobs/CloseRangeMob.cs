using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class CloseRangeMob : CharacterBody2D
{
	
	public const float Speed = 100.0f;
	Vector2 mobPosition = Vector2.Zero;
	Vector2 targetPosition = Vector2.Zero;
	Vector2 playerPosition;	
	public Health health;
	public DamageReceiver damage_receiver{ get; set; }
	public override void _Ready()
	{
		this.health = new Health(40, onDeath);
		Area2D hitbox = GetNode<Area2D>("./Hitbox");
		this.damage_receiver = new DamageReceiver(ref health, ref hitbox, 0.5f);
	}
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
	public override void _Process(double delta)
	{
		damage_receiver.ApplyCollidingDamage((float)delta);
	}

		public void onDeath() {
		this.QueueFree(); // Deletes the Node
	}
	
}

