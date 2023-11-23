using System.Collections.Generic;

namespace crawler.scripts.nodes.world;
using Godot;
using System;
using crawler.scripts.utils.extensions;

public partial class Stairs : Node2D
{
    public enum StairsDirection
    {
        Up,
        Down
    }
    
    [Signal]
    public delegate void HandleEnterEventHandler();

    private Godot.Collections.Dictionary<StairsDirection, Sprite2D> _sprites = new();
    private Sprite2D _currentSprite = null;
    private StairsDirection _direction = StairsDirection.Up;
    
    [Export]
    public StairsDirection Direction
    {
        get => _direction;
        set
        {
            if (_direction != value)
            {
                _direction = value;
                _currentSprite = _sprites.GetOrThrow(value);
                _currentSprite.Visible = true;
                QueueRedraw();
            }
        }
    }

    public override void _Ready()
    {
        _sprites.Add(StairsDirection.Up, GetNode<Sprite2D>("StairsUp"));
        _sprites.Add(StairsDirection.Down, GetNode<Sprite2D>("StairsDown"));
        _currentSprite = _sprites.GetOrThrow(Direction);
        _currentSprite.Visible = true;
    }
}
