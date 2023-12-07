using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class PlayerDashState : State
{
    public new player parent_node;
    public PlayerDashState(player parent_node) : base(parent_node)
    {
        this.parent_node = parent_node;
    }

    
    public float x_strength;
    public float y_strength;

    override public void _EnterState() {
        AnimationPlayer animationPlayer = this.parent_node.GetNode<AnimationPlayer>("./AnimationPlayer");
        //animationPlayer.CurrentAnimation = "idle";      Dash animation
        parent_node.player_movement_velocity *= 3; 
    }

    override public void _ExitState() {

    }
    override public void _InputProcess() {

    }
    double t =0;
    public double elapsedTime = 0;
    override public State _process_state(double delta) {
        elapsedTime += delta;
        if (elapsedTime >= 0.1) {
            return new PlayerIdleState(parent_node);
        }
        if (elapsedTime <= 0.02) {
            //parent_node.player_movement_velocity *= 3 + (5 - 3) * t;
        }
        if (elapsedTime >= 0.08) {
            //parent_node.player_movement_velocity *= 3 + (5 - 3) * t;
        }
        if (elapsedTime >= 0.02 && elapsedTime <= 0.08) {
            //parent_node.player_movement_velocity *= 3 + (5 - 3) * t;
        }

        return null;
    }

    override public State _physics_process_state(double delta) {
        return null;
    }
}