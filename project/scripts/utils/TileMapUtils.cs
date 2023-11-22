using Godot;

namespace crawler.scripts.utils;

public class TileMapUtils
{

    /// <summary>
    /// Converts <c>target</c>, a <c>Vector2</c> in <c>targetOwner</c>'s local space,
    /// into the map space of <c>tileMap</c>.
    /// </summary>
    /// <param name="target">vector describing the desired location</param>
    /// <param name="targetOwner">node whose local space <c>target</c> is in</param>
    /// <param name="tileMap">the tilemap to query</param>
    /// <returns></returns>
    public static Vector2I OtherLocalToMapCoordinates(Vector2 target, Node2D targetOwner, TileMap tileMap)
    {
        // Transform target coordinates into global space using
        // owner's local space
        var cellTarget = targetOwner.ToGlobal(target);
        // Transform to local space of tile map
        var asLocal = tileMap.ToLocal(cellTarget);
        // Get grid coordinates of the tile
        return tileMap.LocalToMap(asLocal);
    }
}