using crawler.scripts.engine.components.character;

namespace crawler.scripts.engine.entity;

public class Player : Entity
{
    public Player()
    {
        AddComponent(new HitPointsComponent(5));
    }
}