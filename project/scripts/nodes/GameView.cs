using Godot;
using System;
using crawler.scripts.engine;

/// <summary>
/// <c>GameView</c> is a singleton scene that is always present in the scene tree.
/// It's a Node2D that contains the entire game world.
/// </summary>
public partial class GameView : Node2D
{
    public static GameView Instance = null;
    
    public override void _EnterTree()
    {
        Instance = this;
        Game.Instance.StartGame();
    }
}
