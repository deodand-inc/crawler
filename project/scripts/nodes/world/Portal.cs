using System;
using crawler.scripts.engine.zones;
using Godot;

namespace crawler.scripts.nodes.world;

// TODO: For some reason, can't get this to work when working with
//  a raw `Portal`.
public partial class Portal : Node2D, IIdentifiable
{
    private Guid InstanceId
    {
        get => Guid.Parse(Id);
    }

    private String _id;
    
    [Export]
    public String Id
    {
        get => _id;
        private set
        {
            GD.Print(value);
            _id = String.IsNullOrEmpty(value) ? Guid.NewGuid().ToString() : value;
        }
    }

    [Export]
    public ZoneLocation Target = new();

    public Guid GetId()
    {
        return InstanceId;
    }
}