using System.Collections.Generic;
using Godot;
using static crawler.scripts.nodes.world.Stairs;

namespace crawler.scripts.input;

/// <summary>
/// Defines the set of possible actions an entity can take.
/// </summary>
public class Actions
{
    public record Action
    {
        /// <summary>
        /// The name of this action in the InputMap.
        /// </summary>
        public StringName Name { get; init; }
        
    }
    
    public record MovementAction : Action {
        
        /// <summary>
        /// The direction to move the entity in, if applicable.
        /// </summary>
        public Vector2 Direction { get; init; }
    }

    public record ZMovementAction : Action
    {
        
        public ZDirection Direction { get; init; }
    }
    
    public static MovementAction MoveNorth = new() { Name = new StringName("move_north"), Direction = Vector2.Up } ;
    public static MovementAction MoveSouth = new() { Name = new StringName("move_south"), Direction = Vector2.Down};
    public static MovementAction MoveWest = new() { Name = new StringName("move_west"), Direction = Vector2.Left};
    public static MovementAction MoveEast = new() { Name = new StringName("move_east"), Direction = Vector2.Right};
    public static MovementAction MoveNortheast = new() { Name = new StringName("move_northeast"), Direction = Vector2.FromAngle(Mathf.DegToRad(-45)) };
    public static MovementAction MoveNorthwest = new() { Name = new StringName("move_northwest"), Direction = Vector2.FromAngle(Mathf.DegToRad(-135)) };
    public static MovementAction MoveSoutheast = new() { Name = new StringName("move_southeast"), Direction = Vector2.FromAngle(Mathf.DegToRad(45)) };
    public static MovementAction MoveSouthwest = new() { Name = new StringName("move_southwest"), Direction = Vector2.FromAngle(Mathf.DegToRad(135)) };
    public static ZMovementAction MoveUp = new() { Name = new StringName("move_up"), Direction = ZDirection.Up };
    public static ZMovementAction MoveDown = new() { Name = new StringName("move_down"), Direction = ZDirection.Down };
    
    public static readonly ISet<Action> ActionsList = new HashSet<Action>()
    {
        MoveNorth,
        MoveSouth,
        MoveWest,
        MoveEast,
        MoveNortheast,
        MoveNorthwest,
        MoveSoutheast,
        MoveSouthwest,
        MoveUp,
        MoveDown
    };
}