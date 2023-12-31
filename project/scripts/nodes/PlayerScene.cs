using System.Collections.Generic;
using crawler.scripts.engine;
using crawler.scripts.engine.entity;
using crawler.scripts.engine.events;
using crawler.scripts.engine.zones;
using crawler.scripts.nodes.world;

namespace crawler.scripts.nodes;

using Godot;
using System;
using System.ComponentModel;
using System.Numerics;
using crawler.scripts.input;
using crawler.scripts.utils;
using Mutex = System.Threading.Mutex;
using Vector2 = Godot.Vector2;

public partial class PlayerScene : Area2D
{
	public static readonly PlayerScene Instance = (PlayerScene) GD.Load<PackedScene>("res://scenes/Player.tscn").Instantiate();

	private bool _debug = false;
	private Rect2 _debugRect;
	private TileMap _parent;
	
	public override void _Draw()
	{
		if (!_debug)
		{
			return;
		}
		if (_debugRect.Size != Vector2.Zero)
		{
			_debugRect.Position -= Position;
			DrawRect(_debugRect, Colors.Aqua, false);	
		}
	}

	private RayCast2D _ray;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Position = Game.Instance.Player.Position;
		// Get a reference to our raycast node.
		_ray = GetNode<RayCast2D>("RayCast2D");
	}

	public override void _ExitTree()
	{
		RequestReady();
	}

	// TODO ARJ: Don't think this mutex actually helps prevent the collision bug
	private static readonly Mutex MovementMutex = new Mutex();
	
	/// <summary>
	/// Called whenever an input is received. 
	/// </summary>
	/// <param name="event">object describing the input</param>
	public override void _Input(InputEvent @event)
	{
		foreach (var action in Actions.ActionsList)
		{
			if (@event.IsActionPressed(action.Name))
			{
				HandleActionPressed(action);
			}
		}
	}

	private void HandleActionPressed(Actions.Action action)
	{
		switch (action)
		{
			case Actions.MovementAction ma:
			{
				try
				{
					MovementMutex.WaitOne();
					_TryMove(ma.Direction);
				}
				finally
				{
					MovementMutex.ReleaseMutex();
				}
				return;
			}
			case Actions.ZMovementAction za:
			{
				var nodes = ZoneService.Instance.CurrentZone.NodesByPosition;
				GD.Print(Position);
				if (!nodes.ContainsKey(Position))
				{
					break;
				}

				var whatsThere = nodes[Position];
				if (whatsThere is Stairs s && s.Direction == za.Direction)
				{
					ZoneService.Instance.MovePlayerToZone(s.Target);
				}
				break;
			}
		}
	}

	private void _TryMove(Vector2 direction)
	{
		if (_debug)
		{
			// Reset the rectangle marking the collided-with object
			// to a size-0 rectangle (i.e. nothing is drawn)
			_debugRect = new Rect2();
			QueueRedraw();
		}
		// We want to target the centre of the tile that `direction` indicates
		// Multiply the direction by the tile size to scale it for the space
		var target = (Position + direction * Constants.TileSize)
			// If the movement is diagonal, the `direction` components won't be whole numbers,
			// which would cause the entity to desync from the grid. 
			// To avoid this, 'snapping' it rounds it up or down to nearest multiple of 16.
			.Snapped(Vector2.One * Constants.TileSize);
		// Set the destination end point for the raycast. These coordinates are relative
		// to the player's, so we need to subtract the player's position from them to
		// normalise them.
		_ray.TargetPosition = target - Position;
		// Force the physics engine to run immediately and see if any intersections
		// occur between our position and the ray's target, and if they don't, 
		// then we can move there.
		_ray.ForceRaycastUpdate();
		if (_ray.IsColliding())
		{
			// In debug mode, draw a rectangle around the collided-with tile
			if (_debug)
			{
				_debugRect = new Rect2(target, 16, 16);
			}

			// Apply any events related to this collision; 
			// if this function returns false, it means the character should not
			// move to this position
			if (!CheckCollision(_ray.GetCollider()))
			{
				return;
			}
		}
		Position = target;
	}

	/// <summary>
	/// Run any events on collision.
	/// </summary>
	/// <returns>whether player should be translated to this tile's position</returns>
	private bool CheckCollision(GodotObject collidedWith)
	{
		switch (collidedWith)
		{
			case Portal p:
			{
				if (p.IsWarp)
				{
					ZoneService.Instance.MovePlayerToZone(p.Target);
					return false;
				}
				return true;
			}
			case TileMap:
			{
				// Bumped into a wall
				return false;
			}
			case DataDrivenEntityScene d:
			{
				d.RouteEvent(new CollisionEvent(Game.Instance.Player, d.Entity));
				// TODO: need to determine whether player can actually move here.
				return true;
			}
		}

		return true;
	}
}
