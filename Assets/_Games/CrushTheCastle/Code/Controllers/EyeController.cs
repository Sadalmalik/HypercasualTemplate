using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeController : MonoBehaviour
{
    [Range(0, 20f)]
    public float damping;
    public Transform[] targets;
    
    public float spring => 1 - damping * 0.05f;
    
    void Update()
    {
        if (targets.Length>0)
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
            transform.rotation = Quaternion.Lerp(transform.rotation, look, spring);
        }
        else
        {
            var look = Quaternion.LookRotation(-transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, look, spring);
        }
    }
}
