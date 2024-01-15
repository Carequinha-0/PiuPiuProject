using Godot;
using System;
using System.Runtime.Serialization;

public partial class Boss : CharacterBody2D
{
	public StateMachine state_machine;

	public Vector2 movement_speed_vector;
	public float movement_speed = 100;
	public AnimatedSprite2D animated_sprite;
	public Health health;
	public DamageReceiver damageReceiver;

	public enum BossAction {
		Rest,
		CloseIn,
		GetAway,
		NormalShot,
		AreaShot
	}
	public override void _Ready()
	{
		state_machine = new StateMachine(new BossGetAwayState(this));
		animated_sprite = GetNode<AnimatedSprite2D>("./AnimatedSprite2D");
		health = new Health(10, onDeath);
		Area2D hitbox = GetNode<Area2D>("./Hitbox");
		damageReceiver = new DamageReceiver(ref health, ref hitbox, 0.1f);
	}
	public void onDeath() {
		QueueFree();
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		velocity = movement_speed_vector;
		Velocity = velocity;
		MoveAndSlide();
		damageReceiver.ApplyCollidingDamage((float)delta);
	}
	public override void _Process(double delta)
	{
		state_machine.Process(delta);
	}

	public BossAction RollAction() {
		Random random = new Random();
		int action = random.Next(0, 4);
		switch (action) {
			case 0:
				return BossAction.CloseIn;
			case 1:
				return BossAction.GetAway;
			case 2:
				return BossAction.NormalShot;
			case 3:
				return BossAction.AreaShot;
			default:
				return BossAction.Rest;
		}
	}

	public string GetAnimationNameByAngle(float angle) {
		var anim_string = "";

		var angle_abs = Math.Abs(angle);

		if(angle < 0) {
			if(angle_abs < Math.PI/8) {
				anim_string = "walk_right";
			} else if (angle_abs < Math.PI/8 * 3) {
				anim_string = "walk_up_right";
			} else if (angle_abs < Math.PI/8 * 5) {
				anim_string = "walk_up";
			} else if (angle_abs < Math.PI/8 * 7) {
				anim_string = "walk_up_left";
			} else if (angle_abs <= Math.PI) {
				anim_string = "walk_left";
			}
		} else {
			if(angle_abs < Math.PI/8) {
				anim_string = "walk_right";
			} else if (angle_abs < Math.PI/8 * 3) {
				anim_string = "walk_down_right";
			} else if (angle_abs < Math.PI/8 * 5) {
				anim_string = "walk_down";
			} else if (angle_abs < Math.PI/8 * 7) {
				anim_string = "walk_down_left";
			} else if (angle_abs <= Math.PI || angle_abs == 0) {
				anim_string = "walk_left";
			}
		} 

		return anim_string;
	}
}
