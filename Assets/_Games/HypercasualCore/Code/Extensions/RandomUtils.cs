using System.Collections.ObjectModel;
using UnityEngine;

public static class RandomUtils
{
    public static T Choice<T>(T[]array)
    {
        return array[Random.Range(0,array.Length)];
    }
    
    public static T ChoiseRandom<T>(this T[] array)
    {
        return array[Random.Range(0,array.Length)];
    }
    
    public static T ChoiseRandom<T>(this Collection<T> array)
    {
        return array[Random.Range(0,array.Count)];
    }
    
    public static float GetRandomIn(float offset)
    {
        return (Random.value - 0.5f) * offset;
    }
    public static Vector2 GetRandomIn(Vector2 offset)
    {
        return new Vector2(
            (Random.value - 0.5f) * offset.x,
            (Random.value - 0.5f) * offset.y
        );
    }
    public static Vector3 GetRandomIn(Vector3 offset)
    {
        return new Vector3(
            (Random.value - 0.5f) * offset.x,
            (Random.value - 0.5f) * offset.y,
            (Random.value - 0.5f) * offset.z
        );
    }
}
