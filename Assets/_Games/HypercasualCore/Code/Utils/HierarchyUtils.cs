using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HierarchyUtils
{
    public static void SafeDestroy(GameObject obj)
    {
        if (Application.isPlaying)
            GameObject.Destroy(obj);
        else
            GameObject.DestroyImmediate(obj);
    }
}
