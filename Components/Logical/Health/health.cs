using System;
using System.Runtime.CompilerServices;
using Godot;

public class Health
{
    public float max_health;
    public float current_health;
    public Action OnDeath;

    public Health(float max_health, Action onDeath) {
        this.max_health = max_health;
        this.current_health = max_health;
        this.OnDeath = onDeath;
    }

    public void TakeDamage(float damage) {
        current_health -= damage;
        GD.Print("Health: " + current_health);
        if (current_health <= 0) {
            OnDeath();
        }
    }
    public void Heal(float heal_amount) {
        current_health += heal_amount;
        if (current_health > max_health) {
            current_health = max_health;
        }
    }
    public void HealFull() {
        this.current_health = max_health;
    }
    public float GetHealthPercentage() {
        return (current_health / max_health) * 100;
    }
}