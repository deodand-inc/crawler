using System;
using System.Collections.Generic;
using crawler.scripts.engine.events;
using Godot;

namespace crawler.scripts.engine.components.character;

public class DamageComponent : AbstractComponent
{
    public int Amount;

    public DamageComponent(Dictionary<string, object> properties) : base(properties)
    {
        Amount = Int32.Parse((string) properties.GetValueOrDefault("damage", "0"));
    }

    public override IList<Type> GetHandledTypes()
    {
        return new List<Type> { typeof(CollisionEvent) };
    }

    public override bool HandleEvent(Event e)
    {
        if (e is not CollisionEvent ce)
        {
            return false;
        }
        ce.collider.HandleEvent(new DamageEvent(Amount));
        return true;
    }
}