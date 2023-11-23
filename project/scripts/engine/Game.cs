using crawler.scripts.engine.entity;
using crawler.scripts.engine.zones;
using crawler.scripts.nodes;
using Godot;

namespace crawler.scripts.engine;

public class Game
{
    private static Game _instance = null;

    public Player Player;
    private ZoneService _zoneService;

    private Game()
    {
        Player = new Player();
        _zoneService = ZoneService.Instance;
    }

    public static Game Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Game();
            }
            return _instance;
        }
    }

    public void StartGame(GameView view)
    {
        var sceneNode = (Node2D) _zoneService.CurrentZone.Scene.Instantiate();
        view.AddChild(sceneNode);
        _zoneService.MovePlayerToZone(Player, sceneNode);
        // TODO ARJ: not right to have to load the player scene here. This should be singleton.
        var player = GD.Load<PackedScene>("res://scenes/Player.tscn");
        sceneNode.AddChild(player.Instantiate());
    }
}