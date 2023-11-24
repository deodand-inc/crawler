﻿using System;
using System.Collections.Generic;
using crawler.scripts.engine.events;

namespace crawler.scripts.engine.components.character;

public class HitPoints : AbstractComponent
{
    public int Value
    {
        get;
        private set;
    }

    public HitPoints(int value)
    {
        Value = value;
    }

    public new IList<Type> GetHandledTypes()
    {
        return new List<Type> { typeof(DamageEvent) };
    }

    public new bool HandleEvent(Event e)
    {
        if (e is not DamageEvent d)
        {
            return false;
        }

        Value -= d.amount;
        return true;
    }
};