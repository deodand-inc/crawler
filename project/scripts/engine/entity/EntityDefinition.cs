using System.Collections.Generic;
using Godot;

namespace crawler.scripts.engine.entity;

public record EntityDefinition
{
    public string Name;
    public string SpritePath;
    public Vector2 SpriteCoordinates;
    public Dictionary<string, Dictionary<string, object>> Components;
}