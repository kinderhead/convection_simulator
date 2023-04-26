using Godot;
using System;

public partial class Parcel : MeshInstance2D
{
    [Export]
    public double Temperature;

    public Label TemperatureLabel;

    public double TempCalculation(double x)
    {
        return -x*x + 1;
    }

	public override void _Ready()
	{
        TemperatureLabel = GetNode<Label>("Temperature");
    }

	public override void _Process(double delta)
	{
        var distToEarth = GlobalPosition.DistanceTo(Vector2.Zero);

        var tempMod = Math.Clamp(Math.Abs(GlobalPosition.Y)/500, 0, 1);
        Temperature += TempCalculation(tempMod) * 7.5 * delta * Math.Clamp(1-((distToEarth-500)/250), 0, 1);
        
        if (Temperature > -15)
        {
            Temperature -= 2 * delta * distToEarth/500;
        }

        TemperatureLabel.Text = ((int)Math.Round(Temperature)).ToString();
	}

    public override void _PhysicsProcess(double delta)
    {
        GetParent<RigidBody2D>().ApplyCentralForce(GlobalPosition.DirectionTo(new(0, 0)) * 500 * (float)delta);

        var effectiveTemp = Temperature;
        if (Temperature > 0)
        {
            //var effectiveTemp = Math.Min(10, Temperature);
            
            GetParent<RigidBody2D>().ApplyCentralForce(-GlobalPosition.DirectionTo(new(0, 0)) * 100 * (float)delta * (float)effectiveTemp);
        }

        if (GlobalPosition.DistanceTo(Vector2.Zero) < 750)
        {
            GetParent<RigidBody2D>().ApplyCentralForce(GlobalPosition.DirectionTo(new Vector2(GlobalPosition.X, 0)) * (float)delta * 300);
        }
        else
        {
            GetParent<RigidBody2D>().ApplyCentralForce(GlobalPosition.DirectionTo(new Vector2(GlobalPosition.X, 500 * Math.Sign(GlobalPosition.Y))) * (float)delta * 300);
        }
    }
}
