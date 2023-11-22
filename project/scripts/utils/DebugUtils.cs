using System;
using System.ComponentModel;
using Godot;

namespace crawler.scripts.utils;

public class DebugUtils
{

    public static void PrintFields(object obj)
    {
        foreach(PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
        {
            string name = descriptor.Name;
            object value = descriptor.GetValue(obj);
            GD.Print(name, "=", value);
        }
    }
}