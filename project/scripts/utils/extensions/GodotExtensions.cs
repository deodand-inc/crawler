using System;
using Godot;
using Godot.Collections;

namespace crawler.scripts.utils.extensions;

public static class GodotExtensions
{
    public static TValue GetOrThrow<[MustBeVariant] TKey, [MustBeVariant] TValue>(
        this Dictionary<TKey, TValue> dict, TKey key)
    {
        return GetOrThrow(dict, key, $"Couldn't find value for key {key}");
    }
    
    public static TValue GetOrThrow<[MustBeVariant] TKey, [MustBeVariant] TValue>(
        this Dictionary<TKey, TValue> dict, TKey key, String message)
    {
        TValue ret;
        if (!dict.TryGetValue(key, out ret))
        {
            throw new Exception(message);
        }

        return ret;
    }
    
    public static bool Has<[MustBeVariant] TKey, [MustBeVariant] TValue>(
        this Dictionary<TKey, TValue> dict, TKey key)
    {
        TValue none;
        return dict.TryGetValue(key, out none);
    }
}