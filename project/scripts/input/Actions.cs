using System.Collections.Generic;
using Godot;

namespace crawler.scripts.input;

/// <summary>
/// Defines the set of possible actions an entity can take.
/// </summary>
public class Actions
{
    public record ActionType
    {
        /// <summary>
        /// The name of this action in the InputMap.
        /// </summary>
        public StringName Name { get; init; }
        
        /// <summary>
        /// (This should be on a subclass)
        /// <br/>
        /// The direction to move the entity in, if applicable.
        /// </summary>
        public Vector2 Direction { get; init; }
    }
    
    public static ActionType MoveNorth = new ActionType { Name = new StringName("move_north"), Direction = Vector2.Up } ;
    public static ActionType MoveSouth = new ActionType { Name = new StringName("move_south"), Direction = Vector2.Down};
    public static ActionType MoveWest = new ActionType { Name = new StringName("move_west"), Direction = Vector2.Left};
    public static ActionType MoveEast = new ActionType { Name = new StringName("move_east"), Direction = Vector2.Right};
    public static ActionType MoveNortheast = new ActionType { Name = new StringName("move_northeast"), Direction = Vector2.FromAngle(Mathf.DegToRad(-45)) };
    public static ActionType MoveNorthwest = new ActionType { Name = new StringName("move_northwest"), Direction = Vector2.FromAngle(Mathf.DegToRad(-135)) };
    public static ActionType MoveSoutheast = new ActionType { Name = new StringName("move_southeast"), Direction = Vector2.FromAngle(Mathf.DegToRad(45)) };
    public static ActionType MoveSouthwest = new ActionType { Name = new StringName("move_southwest"), Direction = Vector2.FromAngle(Mathf.DegToRad(135)) };

    public static readonly ISet<ActionType> ActionsList = new HashSet<ActionType>()
    {
        MoveNorth,
        MoveSouth,
        MoveWest,
        MoveEast,
        MoveNortheast,
        MoveNorthwest,
        MoveSoutheast,
        MoveSouthwest
    };
}