using System;
using crawler.scripts.nodes;
using Godot;
using System.Collections;
using System.Collections.Generic;

namespace crawler.scripts.engine.zones;

public class ActiveZone
{
    public readonly Zone Source;
    public readonly TileMap Map;
    public readonly Vector2 StartPosition;
    public readonly Dictionary<Vector2, Node2D> NodesByPosition;

    private ActiveZone(Zone source, TileMap map, Vector2 startPosition, Dictionary<Vector2, Node2D> nodesByPosition)
    {
        Source = source;
        Map = map;
        StartPosition = startPosition;
        NodesByPosition = nodesByPosition;
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

        var nodesByPosition = new Dictionary<Vector2, Node2D>();
        foreach (var n in map.GetChildren())
        {
            if (n is Node2D asNode2D)
            {
                nodesByPosition.Add(asNode2D.Position, asNode2D);
            }
        }

        return new ActiveZone(source, map, position, nodesByPosition);
    }
}