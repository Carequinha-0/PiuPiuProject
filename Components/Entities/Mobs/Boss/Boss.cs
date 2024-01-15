using Godot;
using System;

public partial class Boss : CharacterBody2D
{
	public StateMachine state_machine;

	public Vector2 movement_speed_vector;
	public float movement_speed = 100;
	public AnimatedSprite2D animated_sprite;

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
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		velocity = movement_speed_vector;
		Velocity = velocity;
		MoveAndSlide();
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
		angle = angle * 180 / (float)Math.PI;
		if (angle > 0 && angle < 45) {
			anim_string = "walk_right";
		} else if (angle > 45 && angle < 90) {
			anim_string = "walk_up_right";
		} else if (angle > 90 && angle < 135) {
			anim_string = "walk_up";
		} else if (angle > 135 && angle < 180) {
			anim_string = "walk_up_left";
		} else if (angle > 180 && angle < 225) {
			anim_string = "walk_left";
		} else if (angle > 225 && angle < 270) {
			anim_string = "walk_down_left";
		} else if (angle > 270 && angle < 315) {
			anim_string = "walk_down";
		} else if (angle > 315 && angle < 360) {
			anim_string = "walk_down_right";
		}

		return anim_string;
	}
}
