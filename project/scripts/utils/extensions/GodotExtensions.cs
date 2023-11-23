using System;
using Godot;
using Godot.Collections;

namespace crawler.scripts.utils.extensions;

public static class GodotExtensions
{
    public static TValue GetOrThrow<[MustBeVariant] TKey, [MustBeVariant] TValue>(this Dictionary<TKey, TValue> dict, TKey key)
    {
        TValue ret;
        if (!dict.TryGetValue(key, out ret))
        {
            throw new Exception($"Couldn't find value for key {key}");
        }

        return ret;
    }
}