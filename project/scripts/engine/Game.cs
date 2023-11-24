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

    public void StartGame()
    {
        _zoneService.MovePlayerToZone(_zoneService.GetStartingZone());
    }
}