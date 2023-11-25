using crawler.scripts.engine.entity;
using crawler.scripts.nodes;
using Godot;

namespace crawler.scripts.engine.events;

public class CollisionEvent : Event
{
    public Entity Collider;
    public Entity CollidedWith;

    public CollisionEvent(Entity collider, Entity collidedWith)
    {
        Collider = collider;
        CollidedWith = collidedWith;
    }
}