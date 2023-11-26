using System;
using System.Collections.Generic;
using crawler.scripts.engine.events;

namespace crawler.scripts.engine.components;

public interface IComponent
{
    public void OnComponentMount();
    
    public void OnComponentUnmount();

    public IList<Type> GetHandledTypes();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    /// <returns>whether this event should be cancelled</returns>
    public bool HandleEvent(Event e);
}
