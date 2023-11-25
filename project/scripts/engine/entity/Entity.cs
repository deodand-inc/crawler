using System;
using System.Collections.Generic;
using crawler.scripts.engine.components;
using crawler.scripts.engine.events;
using crawler.scripts.utils.extensions;
using Godot;

namespace crawler.scripts.engine.entity;

public class Entity
{
    public Vector2 Position;
    
    protected ComponentMap Components = new();

    public void AddComponent(IComponent component)
    {
        Components.Register(component);
    }

    public void RouteEvent(Event e)
    {
        EventUtils.RouteEvent(e, Components);
    }
}