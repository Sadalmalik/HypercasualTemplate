using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeController : MonoBehaviour
{
    public Transform[] targets;
    public float damping;
    
    void Update()
    {
        var dist = 10000000000f;
        var target = (Transform)null;
        foreach (Transform t in targets)
        {
            var d = Vector3.Distance(t.position, transform.position);
            if (d<dist)
            {
                target = t;
                dist = d;
            }
        }
        var look = Quaternion.LookRotation(transform.position-target.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, look, damping);
    }
}
