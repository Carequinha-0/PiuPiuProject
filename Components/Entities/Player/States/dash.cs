using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    Vector2 originalPlayerVelocity;

    override public void _EnterState() {
        AnimationPlayer animationPlayer = this.parent_node.GetNode<AnimationPlayer>("./AnimationPlayer");
        //animationPlayer.CurrentAnimation = "idle";      Dash animation
        parent_node.player_movement_velocity *= 3;
        originalPlayerVelocity = new Vector2(parent_node.player_movement_velocity.X,parent_node.player_movement_velocity.Y);        
    }

    override public void _ExitState() {

    }
    override public void _InputProcess() {

    }
    float t = 0;
    public double elapsedTime = 0; 
    override public State _process_state(double delta) {        
        elapsedTime += delta;

        //Time tables
        if (elapsedTime >= 0.1) {
            parent_node.dashCurrentCooldown = parent_node.dashCooldown;
            return new PlayerIdleState(parent_node);
        }
        if (elapsedTime <= 0.04) {    
            //Interpolations
            parent_node.player_movement_velocity = originalPlayerVelocity * (1 * (1 - t) + 3 * t);
        
            //t's arithmetic progression
            t = (float)elapsedTime * 25;        
        }
        if (elapsedTime >= 0.06) {  
            //Interpolations
            parent_node.player_movement_velocity = originalPlayerVelocity * (1 * (1 - t) + 3 * t);
        
            //t's arithmetic progression
            t = ((float)0.1 - (float)elapsedTime) * 25;          
        }
        if (elapsedTime >= 0.04 && elapsedTime <= 0.06) {
            parent_node.player_movement_velocity = originalPlayerVelocity *3;        
        }
        return null;
    }

    override public State _physics_process_state(double delta) {
        return null;
    }
}