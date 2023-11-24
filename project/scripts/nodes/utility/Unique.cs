using System;
using Godot;

namespace crawler.scripts.nodes.utility;

[Tool]
public partial class Unique : Resource
{
    public Guid Guid
    {
        get => Guid.Parse(_id);
    }
    private String _id;

    [Export]
    public String Id
    {
        get => _id;
        set => _id = value;
    }

    public Unique()
    {
        _id = Guid.NewGuid().ToString();
    }

    public Unique(string id)
    {
        _id = id;
    }
}