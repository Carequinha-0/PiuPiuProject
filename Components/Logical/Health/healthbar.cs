using System;
using Godot;

public class HealthBarMob{
    public Health health;
    public AnimatedSprite2D healthBarSprite;
    public HealthBarMob(ref Health health, AnimatedSprite2D healthBarSprite) {
        this.health = health;
        this.healthBarSprite = healthBarSprite;
    }
    public void UpdateHealthBar() {
        var health_percentage = health.GetHealthPercentage();
        if (health_percentage == 100) {
            healthBarSprite.Frame = 0;
        } else if (health_percentage > 80) {
            healthBarSprite.Frame = 1;
        } else if (health_percentage > 60) {
            healthBarSprite.Frame = 2;
        } else if (health_percentage > 40) {
            healthBarSprite.Frame = 3;
        } else if (health_percentage > 20) {
            healthBarSprite.Frame = 4;
        } else {
            healthBarSprite.Frame = 5;
        }
    }
}