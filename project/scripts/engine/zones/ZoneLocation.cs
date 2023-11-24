using System;
using System.Globalization;
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
    /// Unique ID of the node within the zone.
    /// </summary>
    public Guid Id
    {
        get => Guid.Parse(_identifier);
    }

    private String _identifier;

    [Export]
    public String Identifier
    {
        get => _identifier;
        set => _identifier = value;
    }
}