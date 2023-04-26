using Godot;
using System;
using System.Collections.Generic;

public partial class Earth : Sprite2D
{
    [Export]
    public PackedScene Parcel;

    [Export]
    public Camera2D Camera;

    public override void _Ready()
	{

	}

	public override void _Process(double delta)
	{
        if (Input.IsActionPressed("place"))
        {
            for (int i = 0; i < 5; i++)
            {
                var pos = new Vector2(GD.Randf() * 2 - 1, GD.Randf() * 2 - 1);
                AddParcel(pos);
            }
        }
	}

    public override void _PhysicsProcess(double delta)
    {
        var physics = GetWorld2D().DirectSpaceState;

        if (Input.IsActionPressed("click"))
        {
            var pos = GetGlobalMousePosition();
            if (pos.DistanceTo(Vector2.Zero) < 850)
            {
                AddParcel(pos);
            }
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton ev && ev.IsPressed())
        {
            if (ev.ButtonIndex == MouseButton.WheelUp) Camera.Zoom += new Vector2(.1f, .1f);
            if (ev.ButtonIndex == MouseButton.WheelDown) Camera.Zoom -= new Vector2(.1f, .1f);
        }
    }

    public void AddParcel(Vector2 pos)
    {
        var p = Parcel.Instantiate<RigidBody2D>();
        p.GlobalPosition = pos;
        AddSibling(p);
    }
}
