using Godot;
using System;

public partial class ShootingMob : CharacterBody2D
{
	
	public const float Speed = 100.0f;
	Vector2 mobPosition = Vector2.Zero;
	Vector2 targetPosition = Vector2.Zero;
	Vector2 playerPosition;
	public Health health;
	DamageReceiver damageReceiver;
	public double bulletCooldownTimePassed = 0; 
	public HealthBarMob healthBar;
	public PackedScene bullet_scene = GD.Load<PackedScene>("res://shooting_mob_bullet.tscn");
	public override void _Ready()
	{
		this.health = new Health(80, onDeath);
		Area2D hitbox = GetNode<Area2D>("./Hitbox");
		this.damageReceiver = new DamageReceiver(ref health, ref hitbox, 0.5f);
		this.healthBar = new HealthBarMob(ref health, GetNode<AnimatedSprite2D>("./HealthBar"));

	}
	public override void _PhysicsProcess(double delta)
	{
		//Vetores posição
		Vector2 velocity = Velocity; 
		Vector2 mobPosition = this.Position;		
		Vector2 playerPosition = GetNode<CharacterBody2D>("../Player").Position;
		targetPosition = (playerPosition - mobPosition).Normalized();
		velocity = Vector2.Zero;
		if (mobPosition.DistanceTo(playerPosition) < 10000 && mobPosition.DistanceTo(playerPosition) > 250) {
			velocity = targetPosition;
		}
		velocity = velocity * Speed;
		Velocity = velocity;

		bulletCooldownTimePassed += delta;

		if (bulletCooldownTimePassed > 1) {
			ShootingMobBullet bullet = bullet_scene.Instantiate<ShootingMobBullet>();

			bullet.Position = this.Position + this.Position.DirectionTo(playerPosition);
			bullet.direction = this.Position.DirectionTo(playerPosition);
			bullet.velocity = 100;
			this.AddSibling(bullet);

			bulletCooldownTimePassed = 0;
		}
		MoveAndSlide();
	}
	public override void _Process(double delta)
	{
		damageReceiver.ApplyCollidingDamage((float) delta);
		healthBar.UpdateHealthBar();
	}

	public void onDeath() {
		this.QueueFree(); // Deletes the Node
	}
	
}

