using System;
using System.Collections.Generic;
using crawler.scripts.engine.events;

namespace crawler.scripts.engine.components;

public abstract class AbstractComponent : IComponent
{
    public AbstractComponent()
    {
        OnComponentMount();
    }
    
    public void OnComponentMount()
    {
        EventRouter.Instance.Register(this);
    }

    public void OnComponentUnmount()
    {
        EventRouter.Instance.Deregister(this);
    }
    
    public IList<Type> GetHandledTypes()
    {
        throw new NotImplementedException();
    }

    public bool HandleEvent(Event e)
    {
        throw new NotImplementedException();
    }
}