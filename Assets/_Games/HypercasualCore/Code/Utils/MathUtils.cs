using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtils
{
	public static float Damper(float value, float max=20, float min=0)
	{
	
		return 1 - (Mathf.Clamp(value, min, max) - min) / (max - min);
	}
	
	public static float ExpDamper(float value, float aspect = 5)
	{
		return 1 / Mathf.Exp(value * aspect);
	}
}
