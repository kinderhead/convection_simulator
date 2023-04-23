using Godot;
using System;

public partial class Parcel : MeshInstance2D
{
    [Export]
    public double Temperature;

    public Area2D Detector;

    public Label TemperatureLabel;

	public override void _Ready()
	{
        TemperatureLabel = GetNode<Label>("Temperature");
        Detector = GetParent().GetNode<Area2D>("Detector");
    }

	public override void _Process(double delta)
	{
        var temp_mod = GlobalPosition.X / 500;
        Temperature += temp_mod * .5 * delta;

        TemperatureLabel.Text = ((int)Math.Round(Temperature)).ToString();
	}

    public override void _PhysicsProcess(double delta)
    {
        GetParent<RigidBody2D>().ApplyCentralForce(GlobalPosition.DirectionTo(new(0, 0)) * 500 * (float)delta);

        foreach (var i in Detector.GetOverlappingBodies())
        {
            if (i == GetParent<RigidBody2D>()) continue;

        }
    }
}
