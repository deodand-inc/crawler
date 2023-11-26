using System;
using System.Collections.Generic;
using crawler.scripts.engine.events;

namespace crawler.scripts.engine.components;

public abstract class AbstractComponent : IComponent
{
    protected AbstractComponent()
    {
        OnComponentMount();
    }

    protected AbstractComponent(Dictionary<string, object> properties)
    {
        // Guarantees that all components will have this ctor.
    }
    
    public void OnComponentMount()
    {
        EventRouter.Instance.Register(this);
    }

    public void OnComponentUnmount()
    {
        EventRouter.Instance.Deregister(this);
    }

    public abstract IList<Type> GetHandledTypes();

    public abstract bool HandleEvent(Event e);
}