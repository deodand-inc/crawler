using System.Collections.Generic;
using crawler.scripts.utils;
using Godot;

namespace crawler.scripts.engine.entity;

public record EntityDefinition
{
    public string Name;
    public string SpritePath = Constants.DefaultSpritePath;
    public Vector2 SpriteCoordinates = Constants.VectorNotSet;
    public Dictionary<string, Dictionary<string, object>> Components;
}