using System;
using System.Globalization;
using crawler.scripts.nodes.utility;
using Godot;

namespace crawler.scripts.engine.zones;

/// <summary>
/// An object that uniquely identifies a node in the world.
/// </summary>
public partial class ZoneLocation : Resource
{
    /// <summary>
    /// Name of the zone in which this node is located.
    /// </summary>
    [Export]
    public StringName ZoneName;

    /// <summary>
    /// Unique name of the node within the zone.
    /// </summary>
    [Export]
    public StringName UniqueName;
}