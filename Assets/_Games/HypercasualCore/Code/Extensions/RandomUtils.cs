using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomUtils
{
    public static T Choice<T>(T[]array)
    {
        return array[Random.Range(0,array.Length)];
    }
}
