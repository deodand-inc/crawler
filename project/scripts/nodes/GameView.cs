using Godot;
using System;
using crawler.scripts.engine;

public partial class GameView : Node2D
{
    public override void _EnterTree()
    {
        Game g = Game.Instance;
        g.StartGame(this);
    }
}
