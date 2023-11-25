using System;
using System.Collections.Generic;
using crawler.scripts.engine.components;
using crawler.scripts.utils.extensions;

namespace crawler.scripts.engine.events;

public class EventUtils
{
    public static void RouteEvent(Event e, ComponentMap _components)
    {
        Type t = e.GetType();
        while (t is not null && t != typeof(object))
        {
            foreach (var component in _components.GetComponents(t))
            {
                bool shouldCancel = component.HandleEvent(e);
                if (shouldCancel)
                {
                    return;
                }
            }

            // Proceed up through the type hierarchy as there could be handlers for our
            // base class
            t = t.BaseType;
        }
    }
}