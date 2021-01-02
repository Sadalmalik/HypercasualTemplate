using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisController : MonoBehaviour
{
	public Axis axis;
	public Transform target; 
	public float angle;
	public float angularSpeed;
	public bool limit;
	public float min;
	public float max;

	void Start()
	{
		target.SetAxis(axis, angle);
	}

	public void Move(float amount)
	{
		angle += amount * angularSpeed * Time.deltaTime;
		if (limit)
			angle = Mathf.Clamp(angle, min, max);
		target.SetAxis(axis, angle);
	}
}