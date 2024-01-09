using Godot;
using System;
using System.Collections.Generic;

public class PlayerIdleState : State
{
    public new player parent_node;
    public PlayerIdleState(player parent_node) : base(parent_node)
    {
        this.parent_node = parent_node;
    }

    public float x_strength;
    public float y_strength;

    override public void _EnterState() {
        AnimationPlayer animationPlayer = this.parent_node.GetNode<AnimationPlayer>("./AnimationPlayer");
        animationPlayer.CurrentAnimation = "idle";
        parent_node.player_movement_velocity = Vector2.Zero;
    }

    override public void _ExitState() {

    }
    override public void _InputProcess() {
    }

    override public State _process_state(double delta) {
        x_strength = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
        y_strength = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");
        Vector2 speed_vector = new Vector2
        {
            X = x_strength,
            Y = y_strength
        };
        if(speed_vector != Vector2.Zero)
            return new PlayerWalkingState(parent_node);
        
        if(Input.IsActionJustPressed("shoot") && parent_node.normalShotCurrentCooldown == 0) {
            return new PlayerShootingState(parent_node);
        }

        if (Input.IsActionJustPressed("slashAttack")) {
            return new PlayerSlashState(parent_node);
        }
        
        return null;
    }

    override public State _physics_process_state(double delta) {
        return null;
    }
}