using Godot;
using System;

public partial class BigTitle : Node2D
{
	public string text = "";
	public Label label;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.label = this.GetNode<Label>("./CanvasLayer/Label");
		this.label.Text = this.text;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.label.Text = this.text;
	}

	public void SetText(string text)
	{
		this.text = text;
	}

	public void ToAnimateOpacity(float opacity, float duration)
	{
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(this.label, "modulate:a", opacity, duration);
	}
}
