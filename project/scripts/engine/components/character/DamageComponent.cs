using System;
using System.Collections.Generic;
using crawler.scripts.engine.events;

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
        throw new NotImplementedException();
    }
}