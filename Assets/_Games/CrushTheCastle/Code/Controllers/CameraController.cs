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
    
    void FixedUpdate()
    {
        if (lookTarget!=null)
        {
            var lookat = Quaternion.LookRotation(lookTarget.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookat, MathUtils.ExpDamper(lookDamping, 0.25f));
        }
        
        if (moveTarget!=null)
        {
            transform.position = Vector3.Lerp(transform.position, moveTarget.position, MathUtils.ExpDamper(moveDamping, 0.25f));
        }
    }
}
