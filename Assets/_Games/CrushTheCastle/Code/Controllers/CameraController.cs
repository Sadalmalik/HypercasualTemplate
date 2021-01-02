using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform lookTarget;
    [Range(0f,20f)]
    public float lookDamping;
    [Space]
    public Transform moveTarget;
    [Range(0f,20f)]
    public float moveDamping;
    
    void Update()
    {
        if (lookTarget!=null)
        {
            var lookat = Quaternion.LookRotation(lookTarget.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookat, MathUtils.Damper(lookDamping));
        }
        
        if (moveTarget!=null)
        {
            transform.position = Vector3.Lerp(transform.position, moveTarget.position, MathUtils.Damper(moveDamping));
        }
    }
}
