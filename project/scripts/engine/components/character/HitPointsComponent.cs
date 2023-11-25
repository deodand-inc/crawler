using System;
using System.Collections.Generic;
using crawler.scripts.engine.events;
using Godot;

namespace crawler.scripts.engine.components.character;

public class HitPointsComponent : AbstractComponent
{
    public int Value
    {
        get;
        private set;
    }

    public HitPointsComponent(int value)
    {
        Value = value;
    }

    public override IList<Type> GetHandledTypes()
    {
        return new List<Type> { typeof(DamageEvent) };
    }

    public override bool HandleEvent(Event e)
    {
        if (e is not DamageEvent d)
        {
            return false;
        }

        Value -= d.Amount;
        GD.Print($"Took {d.Amount} damage, health: {Value}");
        return true;
    }
};