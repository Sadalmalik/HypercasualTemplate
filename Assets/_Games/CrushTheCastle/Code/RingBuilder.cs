using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RingBuilder : MonoBehaviour
{
	public Transform prefab;
	public float width;
	public float radius;
	public float baseAngle;

	[Space]
	public bool rebuild;

	public bool loop;
	public bool clear;

	void Update()
	{
		if (rebuild)
		{
			rebuild = loop;
			Rebuild();
		}

		if (clear)
		{
			clear = false;
			transform.DestroyChilds();
		}
	}

	public void Rebuild()
	{
		transform.DestroyChilds();

		var len   = 2 * Mathf.PI * radius;
		var steps = Mathf.FloorToInt(len / width);
		for (int i = 0; i < steps; i++)
		{
			var angle = 360f * i / steps;
			var item  = Instantiate(prefab, transform);
			item.localPosition = new Vector3(
					radius * Mathf.Cos(Mathf.Deg2Rad * angle), 0,
					radius * Mathf.Sin(Mathf.Deg2Rad * angle)
				);
			item.localRotation = Quaternion.Euler(0, -angle + baseAngle, 0);
		}
	}
}