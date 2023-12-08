using System;
using Godot;

public class DamageReceiver
{
    public Health health;
    public Area2D hitbox;
    public DamageReceiver(ref Health health, ref Area2D hitbox) {
        this.health = health;
        this.hitbox = hitbox;
    }
    public void ApplyCollidingDamage() {
        var bodies = hitbox.GetOverlappingAreas();
        foreach (var body in bodies) {
            DamageSource body_dealer = body as DamageSource;
            if (body != null) {
                health.TakeDamage(body_dealer.damage);
            }
        }
    }
}