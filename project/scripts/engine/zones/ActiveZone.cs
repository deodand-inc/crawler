using crawler.scripts.nodes;
using Godot;

namespace crawler.scripts.engine.zones;

public class ActiveZone
{
    public readonly Zone Source;
    public readonly TileMap Map;
    public readonly Vector2 StartPosition;

    private ActiveZone(Zone source, TileMap map, Vector2 startPosition)
    {
        this.Source = source;
        this.Map = map;
        this.StartPosition = startPosition;
    }

    public static ActiveZone MakeActive(Zone source)
    {
        TileMap map = source.Scene.Instantiate<TileMap>();
        Vector2 position = Vector2.Zero;
        if (map.HasNode("PlayerStart"))
        {
            Marker2D startMarker = map.GetNode<Marker2D>("PlayerStart");
            position = startMarker.Position;
        }

        return new ActiveZone(source, map, position);
    }
}