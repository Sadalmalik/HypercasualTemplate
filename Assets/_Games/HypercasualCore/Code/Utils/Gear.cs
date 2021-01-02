using UnityEngine;

// ReSharper disable UnassignedField.Global
// ReSharper disable MemberCanBePrivate.Global

[ExecuteAlways]
public class Gear : MonoBehaviour
{
    public Transform target;
    public Axis fromAxis;
    public Axis toAxis;
    public float fromRadius;
    public float toRadius;
    public float angleOffset;
    
    void Update()
    {
        if (target!=null)
        {
           var axis = target.GetAxis(fromAxis);
           transform.SetAxis(toAxis, angleOffset - axis * fromRadius / toRadius);
        }
    }
}
