using Godot;

public abstract partial class DamageSource : Area2D
{
    public float damage;
    public abstract void OnDamageDealt();
}