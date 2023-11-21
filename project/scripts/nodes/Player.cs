using Godot;
using System;
using System.ComponentModel;
using System.Numerics;
using crawler.scripts.input;
using crawler.scripts.utils;
using Vector2 = Godot.Vector2;

public partial class Player : Area2D
{

	private RayCast2D _ray;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_ray = GetNode<RayCast2D>("RayCast2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _Input(InputEvent @event)
	{
		foreach (var action in Actions.ActionsList)
		{
			if (@event.IsActionPressed(action.Name))
			{
				_TryMove(action.Direction);
			}
		}
	}

	private void _TryMove(Vector2 direction)
	{
		var target = (Position + direction * Constants.TileSize).Snapped(Vector2.One * Constants.TileSize);
		_ray.TargetPosition = target - Position;
		_ray.ForceRaycastUpdate();
		if (!_ray.IsColliding())
		{			
			Position = target;
		}
	}
}
