using System;
using Godot;
using Godot.Collections;

namespace crawler.scripts.utils.extensions;

public static class CrawlerExtensions
{
    // A nice one taken from https://stackoverflow.com/a/63481271
    public static TValue GetOrCompute<TKey, TValue>(
        this System.Collections.Generic.Dictionary<TKey, TValue> dict,
        TKey key,
        Func<TKey, TValue, TValue> func)
    {
        {
            // if no func given, throw.
            if (func == null) throw new ArgumentNullException(nameof(func));
            // if no mapping, return null.
            if (dict.TryGetValue(key, out var value)) return value;
            // get the new value from func.
            var computed = func(key, value);
            if (computed == null)
            {
                // if the mapping exists but func => null,
                // remove the mapping and return null.
                dict.Remove(key);
                return default;
            }
            // mapping exists and func returned a non-null value.
            // set and return the new value
            dict[key] = computed;
            return computed;
        }
    }
    
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