using Godot;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public partial class PlayerShootingState : State
{
    public new player parent_node;
    public PlayerShootingState(player parent_node) : base(parent_node)
    {
        this.parent_node = parent_node;
    }
    public float x_strength;
    public float y_strength;
    public Vector2 mouse_pos;

    public PackedScene bullet_scene = GD.Load<PackedScene>("res://Components/Entities/Objects/NormalShot/normal_shot.tscn");

    override public void _EnterState() {
        AnimationPlayer animationPlayer = parent_node.GetNode<AnimationPlayer>("./AnimationPlayer");
        animationPlayer.CurrentAnimation = "shoot";

       

        mouse_pos = parent_node.GetGlobalMousePosition();
        NormalShot bullet = bullet_scene.Instantiate<NormalShot>();
        bullet.Position = parent_node.Position + parent_node.Position.DirectionTo(mouse_pos) * 40;
        bullet.direction = parent_node.Position.DirectionTo(mouse_pos);
        bullet.velocity = 100;
        this.parent_node.AddSibling(bullet);

    }

    override public void _ExitState() {
        parent_node.normalShotCurrentCooldown = parent_node.normalShotCooldown;
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
        speed_vector = speed_vector.Normalized();
        parent_node.player_movement_velocity = speed_vector * parent_node.SPEED;


        return new PlayerWalkingState(parent_node);
    }

    override public State _physics_process_state(double delta) {
        return null;
    }
}