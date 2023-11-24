using Godot;

namespace crawler.scripts.engine.zones;

public partial class Zone : RefCounted
{
    public StringName ScenePath;

    private PackedScene _scene = null;

    public PackedScene Scene
    {
        get
        {
            if (_scene is null)
            {
                _scene = GD.Load<PackedScene>(ScenePath);
            }

            return _scene;
        }
    }

    public Zone(StringName scenePath)
    {
        ScenePath = scenePath;
    }
}