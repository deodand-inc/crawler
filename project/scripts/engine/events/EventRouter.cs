using System;
using System.Collections.Generic;
using crawler.scripts.engine.components;
using crawler.scripts.utils.extensions;

namespace crawler.scripts.engine.events;

public class EventRouter
{
    private static EventRouter _instance;

    public static EventRouter Instance
    {
        get 
        {
            if (_instance == null)
            {
                _instance = new EventRouter();
            }

            return _instance;
        }
    }

    private ComponentMap _componentRegistry;

    private EventRouter()
    {
        _componentRegistry = new();
    }

    public void Register(IComponent component)
    {
        _componentRegistry.Register(component);
    }

    public void Deregister(IComponent component)
    {
        _componentRegistry.Deregister(component);
    }

    public void RouteEvent(Event e)
    {
        EventUtils.RouteEvent(e, _componentRegistry);
    }
}