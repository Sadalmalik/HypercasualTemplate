using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Gear : MonoBehaviour
{
    public Transform target;
    public Axis fromAxis;
    public Axis toAxis;
    public float fromRadius;
    public float toRadius;
    
    void Update()
    {
        if (target!=null)
        {
           var axis = target.GetAxis(fromAxis);
           transform.SetAxis(toAxis, -axis * fromRadius / toRadius);
        }
    }
}
