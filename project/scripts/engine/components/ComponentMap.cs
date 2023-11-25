using System;
using System.Collections.Generic;
using crawler.scripts.engine.components;
using crawler.scripts.utils.extensions;

namespace crawler.scripts.engine.components;

public class ComponentMap
{
    private Dictionary<Type, ISet<IComponent>> _store = new();
    
    public void Register(IComponent component)
    {
        foreach (var type in component.GetHandledTypes())
        {
            _store.GetOrCompute(type, (_, _) => new HashSet<IComponent>()).Add(component);
        }
    }

    public void Deregister(IComponent component)
    {
        foreach (var type in component.GetHandledTypes())
        {
            _store.GetOrCompute(type, (_, _) => new HashSet<IComponent>()).Remove(component);
        }
    }

    public ISet<IComponent> GetComponents(Type t)
    {
        return _store.GetOrCompute(t, (_, _) => new HashSet<IComponent>());
    }
}