using System;
using Godot;

public class DamageReceiver
{
	public Health health;
	public Area2D hitbox;
	public float damage_cooldown;
	public float timePassed = 0;
	public DamageReceiver(ref Health health, ref Area2D hitbox, float damage_cooldown) {
		this.health = health;
		this.hitbox = hitbox;
		this.damage_cooldown = damage_cooldown;
		timePassed = damage_cooldown;
	}
	public void ApplyCollidingDamage(float delta) {
		var bodies = hitbox.GetOverlappingAreas();

		foreach (var body in bodies) {
			DamageSource body_dealer = body as DamageSource;
			if (body != null) {
				if (timePassed >= damage_cooldown) {
					health.TakeDamage(body_dealer.damage);
					body_dealer.OnDamageDealt();
					timePassed = 0;
				}
			}
		}
		
		timePassed += delta;
	}
}
