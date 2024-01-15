using Godot;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

public class BossGetAwayState : State
{
    public new Boss parent_node;
    public player player_node;
    public double waiting_time = 0;
    public BossGetAwayState(Boss parent_node) : base(parent_node)
    {
        this.parent_node = parent_node;
    }

    override public void _EnterState() {
        player_node = parent_node.GetNode<player>("../Player");
    }

    override public void _ExitState() {

    }
    override public void _InputProcess() {

    }

    override public State _process_state(double delta) {
        parent_node.movement_speed_vector = Vector2.Zero;
        if(parent_node.GlobalPosition.DistanceTo(player_node.GlobalPosition) > 300) {
            parent_node.movement_speed_vector = GetMovementToPlayerSpeed();
        } else if (parent_node.GlobalPosition.DistanceTo(player_node.GlobalPosition) < 250) {
            parent_node.movement_speed_vector = GetMovementAwayFromPlayerSpeed();
        }

        const double SWITCH_ACTION_TIME = 2;
        waiting_time += delta;

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
                default:
                    return new BossGetAwayState(this.parent_node);
            }
        }
        
        return null;
    }

    override public State _physics_process_state(double delta) {
        return null;
    }

    public Vector2 GetMovementToPlayerSpeed() {
        Vector2 player_position = player_node.GlobalPosition;
        Vector2 boss_position = parent_node.GlobalPosition;
        Vector2 direction = boss_position.DirectionTo(player_position);

        return direction * parent_node.movement_speed;
    }
    public Vector2 GetMovementAwayFromPlayerSpeed() {
        Vector2 player_position = player_node.GlobalPosition;
        Vector2 boss_position = parent_node.GlobalPosition;
        Vector2 direction = boss_position.DirectionTo(player_position);

        return direction * -parent_node.movement_speed;
    }
}