using Godot;
using System;
using System.Collections.Generic;

public class BossNormalShotState : State
{
    public new Boss parent_node;
    public player player_node;
    public PackedScene normal_boss_shot_scene = GD.Load<PackedScene>("res://Components/Entities/Mobs/Boss/BossShot/boss_shot.tscn");

    public double waiting_time = 0;
    public double shot_current_cooldown = 0;
    public BossNormalShotState(Boss parent_node) : base(parent_node)
    {
        this.parent_node = parent_node;
    }

    override public void _EnterState() {
        player_node = parent_node.GetNode<player>("../Player");
        parent_node.movement_speed_vector = Vector2.Zero;
    }

    override public void _ExitState() {

    }
    override public void _InputProcess() {
    }

    override public State _process_state(double delta) {
        const double SWITCH_ACTION_TIME = 2;
        const double SHOT_COOLDOWN = 0.2;
        waiting_time += delta;
        shot_current_cooldown += delta;

        if(waiting_time > SWITCH_ACTION_TIME) {
            var new_action = this.parent_node.RollAction();
            switch (new_action) {
                case Boss.BossAction.CloseIn:
                    return new BossGetCloseState(this.parent_node);
                case Boss.BossAction.GetAway:
                    return new BossGetAwayState(this.parent_node);
                case Boss.BossAction.NormalShot:
                    return new BossNormalShotState(this.parent_node);
                case Boss.BossAction.AreaShot:
                    return new BossAreaShotState(this.parent_node);
            }
        }
        
        if(shot_current_cooldown > SHOT_COOLDOWN) {
            shot_current_cooldown = 0;
            BossShot shot = normal_boss_shot_scene.Instantiate<BossShot>();

            shot.velocity = 100;
            shot.direction = parent_node.GlobalPosition.DirectionTo(player_node.GlobalPosition);
            shot.Position = parent_node.GlobalPosition;
            shot.Rotate(shot.direction.Angle());

            this.parent_node.AddSibling(shot);
            parent_node.animated_sprite.Animation = parent_node.GetAnimationNameByAngle(shot.direction.Angle());
        }
        
        return null;
    }

    override public State _physics_process_state(double delta) {
        return null;
    }
}