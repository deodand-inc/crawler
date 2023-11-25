using crawler.scripts.engine.entity;
using crawler.scripts.engine.zones;
using crawler.scripts.nodes;
using Godot;

namespace crawler.scripts.engine;

public class Game
{
    private static Game _instance = null;

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

    public Player Player;
    private ZoneService _zoneService;

    private Game()
    {
        Player = new Player();
        _zoneService = ZoneService.Instance;
    }

    public void StartGame()
    {
        var entity = EntityService.Instance.LoadEntity("MagicTrap");
        _zoneService.MovePlayerToZone(_zoneService.GetStartingZone());
        entity.Position = new Vector2(48, 48);
        _zoneService.CurrentZone.Map.AddChild(entity);
    }
}