using Godot;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public partial class PlayerWalkingState : State
{
    public new player parent_node;
    public PlayerWalkingState(player parent_node) : base(parent_node)
    {
        this.parent_node = parent_node;
    }
    public float x_strength;
    public float y_strength;

    override public void _EnterState() {
    }

    override public void _ExitState() {

    }
    override public void _InputProcess() {
        x_strength = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
        y_strength = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");
    }
    
    override public State _process_state(double delta) {
        Vector2 speed_vector = new Vector2
        {
            X = x_strength,
            Y = y_strength
        };
        if(speed_vector == Vector2.Zero){
            return new PlayerIdleState(parent_node);}
        speed_vector = speed_vector.Normalized();
        parent_node.player_movement_velocity = speed_vector * parent_node.SPEED;

        AnimationPlayer animationPlayer = this.parent_node.GetNode<AnimationPlayer>("./AnimationPlayer");
        animationPlayer.CurrentAnimation = GetAnimationNameBySpeed(speed_vector);
        if (Input.IsActionJustPressed("dash") && parent_node.dashCurrentCooldown == 0) {
            return new PlayerDashState(parent_node);
        }

        if(Input.IsActionJustPressed("shoot") && parent_node.normalShotCurrentCooldown == 0) {
            return new PlayerShootingState(parent_node);
        }
        if (Input.IsActionJustPressed("slashAttack") ) {
            return new PlayerSlashState(parent_node);
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