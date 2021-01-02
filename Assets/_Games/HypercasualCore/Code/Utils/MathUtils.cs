using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtils
{
	public static float Damper(float value, float max=20, float min=0)
	{
		var aspect = (Mathf.Clamp(value, min, max) - min) / (max - min);
		return 1 - aspect * aspect;
	}
}
