namespace crawler.scripts.nodes.world;
using Godot;
using System;
using utils.extensions;

[Tool]
public partial class Stairs : Portal
{
    public enum ZDirection
    {
        Up,
        Down
    }

    private Godot.Collections.Dictionary<ZDirection, Sprite2D> _sprites = new();
    private Sprite2D _currentSprite = null;
    private ZDirection _direction = ZDirection.Up;
    
    [Export]
    public ZDirection Direction
    {
        get => _direction;
        set
        {
            _ensureSpritesLoaded();
            if (_direction != value)
            {
                _direction = value;
                if (_currentSprite != null)
                {
                    _currentSprite.Visible = false;
                    _currentSprite.QueueRedraw();
                }
                _currentSprite = _sprites.GetOrThrow(value);
                _currentSprite.Visible = true;
                _currentSprite.QueueRedraw();
            }
        }
    }

    public Stairs()
    {
        IsWarp = false;
    }

    public override void _Ready()
    {
        _ensureSpritesLoaded();
    }

    public void _ensureSpritesLoaded()
    {
        if (_sprites.Count == 0)
        {
            _sprites.Add(ZDirection.Up, GetNode<Sprite2D>("StairsUp"));
            _sprites.Add(ZDirection.Down, GetNode<Sprite2D>("StairsDown"));
        }
    }
}
