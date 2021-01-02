using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class Level : MonoBehaviour
{
    public float radius;
    public float cannonballSpeed;
    
    public void OnDrawGizmos()
    {
        var temp = Handles.color;
        Handles.color = Color.yellow;
        Handles.CircleHandleCap(0, transform.position, Quaternion.Euler(90,0,0), radius, EventType.Repaint);
        Handles.color = temp;
    }
    
    public Tween DoHeight(float yPosition, float duration)
    {
        var newPos = transform.position;
        newPos.y = yPosition;
        return transform.DOMove(newPos, duration);
    }
}
