using Godot;
using System;
using System.Collections.Generic;

public partial class Earth : Sprite2D
{
    [Export]
    public PackedScene Parcel;

    public override void _Ready()
	{

	}

	public override void _Process(double delta)
	{
        
	}

    public override void _PhysicsProcess(double delta)
    {
        var physics = GetWorld2D().DirectSpaceState;

        if (Input.IsActionPressed("click"))
        {
            var p = Parcel.Instantiate<RigidBody2D>();
            p.GlobalPosition = GetGlobalMousePosition();
            AddSibling(p);
        }
    }
}
