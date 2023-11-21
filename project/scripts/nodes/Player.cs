using Godot;
using System;
using System.Numerics;
using crawler.scripts.input;
using crawler.scripts.utils;
using Vector2 = Godot.Vector2;

public partial class Player : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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
		Position += direction * Constants.TileSize;
		Position = Position.Snapped(Vector2.One * Constants.TileSize);
	}
}
