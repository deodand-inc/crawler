using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using crawler.scripts.engine.components;
using crawler.scripts.nodes.world;
using crawler.scripts.utils;
using Godot;

namespace crawler.scripts.engine.entity;

public class EntityService
{
    private static EntityService _instance = null;

    private Dictionary<string, string> _entityDefs;
    private Dictionary<string, Type> _componentDefs;

    public static EntityService Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EntityService();
            }
            return _instance;
        }
    }

    private EntityService()
    {
        _entityDefs = LoadEntityDefs();
        _componentDefs = LoadComponentDefs();
    }

    private static Dictionary<string, string> LoadEntityDefs()
    {
        const string path = "res://definitions";
        using var dir = DirAccess.Open(path);
        var ret = new Dictionary<string, string>();
        if (dir != null)
        {
            dir.ListDirBegin();
            string fileName = dir.GetNext();
            while (fileName != "")
            {
                if (dir.CurrentIsDir())
                {
                    GD.Print($"Found directory: {fileName}");
                }
                else
                {
                    var sanitisedFileName = fileName.Replace(".yaml", "");
                    var pathName = Godot.ProjectSettings.GlobalizePath(path + "/" + fileName);
                    ret.Add(sanitisedFileName, pathName);
                }

                fileName = dir.GetNext();
            }
        }

        return ret;
    }

    private static Dictionary<string, Type> LoadComponentDefs()
    {
        var ret = new Dictionary<string, Type>();
        var componentType = typeof(IComponent);
        var assembly = componentType.Assembly;
        var types = assembly.GetTypes().Where(t => t.IsAssignableTo(componentType));
        foreach (var type in types)
        {
            ret.Add(type.Name, type);
        }

        return ret;
    }

    public DataDrivenEntity LoadEntity(string entityName)
    {
        if (!_entityDefs.ContainsKey(entityName))
        {
            throw new Exception($"Entity with name ${entityName} not found");
        }

        var defStr = File.ReadAllText(_entityDefs[entityName]);
        var def = YamlParser.Instance.Parse<EntityDefinition>(defStr);
        return Convert(def);
    }

    public DataDrivenEntity Convert(EntityDefinition def)
    {
        var entity = new Entity();
        
        foreach (var c in def.Components)
        {
            if (!_componentDefs.ContainsKey(c.Key))
            {
                continue;
            }

            var type = _componentDefs[c.Key];
            var ctor = type.GetConstructor(new[] { typeof(Dictionary<string, object>) });
            if (ctor is null)
            {
                throw new Exception($"Couldn't find parameterised ctor for type {type.Name}");
            }
            var component = (IComponent) ctor.Invoke(new[] { c.Value });
            entity.AddComponent(component);
        }

        return new DataDrivenEntity(entity);
    }
}