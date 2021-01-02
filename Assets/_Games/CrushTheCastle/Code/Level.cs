using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class Level : MonoBehaviour
{
    public float radius;
    public float cannonBallStartVelocity;
    
    public void OnDrawGizmos()
    {
        var temp = Handles.color;
        Handles.color = Color.yellow;
        Handles.CircleHandleCap(0, transform.position, Quaternion.Euler(90,0,0), radius, EventType.Repaint);
        Handles.color = temp;
    }
    
    public Tween DOMove(Vector3 position, float duration)
    {
        return transform.DOMove(position, duration);
    }
}
