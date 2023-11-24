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

    private Dictionary<Type, ISet<IComponent>> _componentRegistry;

    private EventRouter()
    {
        _componentRegistry = new Dictionary<Type, ISet<IComponent>>();
    }

    public void Register(IComponent component)
    {
        foreach (var type in component.GetHandledTypes())
        {
            _componentRegistry.GetOrCompute(type, (_, _) => new HashSet<IComponent>()).Add(component);
        }
    }

    public void Deregister(IComponent component)
    {
        foreach (var type in component.GetHandledTypes())
        {
            _componentRegistry.GetOrCompute(type, (_, _) => new HashSet<IComponent>()).Remove(component);
        }
    }

    public void RouteEvent(Event e)
    {
        Type t = e.GetType();
        while (t is not null && t != typeof(object))
        {
            foreach (var component in _componentRegistry.GetOrCompute(t, (_, _) => new HashSet<IComponent>()))
            {
                bool shouldCancel = component.HandleEvent(e);
                if (shouldCancel)
                {
                    return;
                }
            }

            // Proceed up through the type hierarchy as there could be handlers for our
            // base class
            t = t?.BaseType;
        }
    }
}