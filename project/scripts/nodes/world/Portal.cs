namespace crawler.scripts.nodes.world;

using Godot;
using System;
using crawler.scripts.engine.zones;

public partial class Portal : Area2D
{
    [Export] public ZoneLocation Target = new();
}
