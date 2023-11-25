using crawler.scripts.nodes;
using Godot;

namespace crawler.scripts.engine.events;

public class CollisionEvent : Event
{
    public PlayerScene collider;
    public GodotObject collidedWith;

    public CollisionEvent(PlayerScene collider, GodotObject collidedWith)
    {
        this.collider = collider;
        this.collidedWith = collidedWith;
    }
}