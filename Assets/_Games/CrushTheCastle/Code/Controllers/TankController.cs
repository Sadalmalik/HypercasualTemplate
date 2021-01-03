using UnityEngine;

// ReSharper disable UnassignedField.Global
// ReSharper disable MemberCanBePrivate.Global

public class TankController : MonoBehaviour
{
    public Transform tank;
    public AxisController body;
    public AxisController cannon;
    public Gear[] wheels;
    public float moveSpeed;
    public float radius;
    
    public void SetRadius(float radius, int gearSteps)
    {
        this.radius = radius;
        
		var len   = 2 * Mathf.PI * radius;
        body.angularSpeed = 360 * moveSpeed / len;
        
        tank.localPosition = new Vector3(0,0, -radius);

        foreach (var wheel in wheels)
            wheel.fromRadius = gearSteps;
    }
    
    public void Move(Vector2 amount)
    {
        body.Move(amount.x);
        cannon.Move(amount.y);
    }
}
