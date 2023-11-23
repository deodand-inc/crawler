using crawler.scripts.engine;
using crawler.scripts.engine.entity;
using crawler.scripts.engine.zones;
using crawler.scripts.nodes.world;
using crawler.scripts.utils.extensions;

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
	private Player _player;
	
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
		_player = Game.Instance.Player;
		Position = _player.Position;
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
				// TODO: Offset of player vs offset of stairs is inconsistent, investigate why
				var nodes = ZoneService.Instance.CurrentZone.Nodes;
				GD.Print(Position);
				if (!nodes.Has(Position))
				{
					break;
				}

				var whatsThere = nodes.GetOrThrow(Position);
				if (whatsThere is Stairs s && s.Direction == za.Direction)
				{
					ZoneService.Instance.MovePlayerToZone(s.SceneName, Position);
				}
				break;
			}
		}
	}

	private void _TryMove(Vector2 direction)
	{
		_debugRect = new Rect2();
		QueueRedraw();
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
		if (!_ray.IsColliding())
		{			
			Position = target;
			return;
		}
		// At this point, you know that the entity has collided with something.
		// Now you could extract it and take different actions depending on what it is.
		// If in 'debug' mode (set this boolean to true and rebuild), draw a rectangle
		// around the collided-with tile.
		if (_debug)
		{
			_debugRect = new Rect2(target, 16, 16);
		}
		var collidedWith = _ray.GetCollider();
		switch (collidedWith)
		{
			// For example
			// case Monster m:
			// {
			//     // Attack the monster if engaged in combat
			//     ...
			// }
			case TileMap tileMap:
			{
				var mapTarget = TileMapUtils.OtherLocalToMapCoordinates(_ray.TargetPosition, _ray, tileMap);
				var sourceId = tileMap.GetCellSourceId(Constants.ForegroundLayer, mapTarget);
				if (sourceId != -1)
				{
					GD.Print(mapTarget, ", ", sourceId);
				}
				break;
			}
		}
	}
}
