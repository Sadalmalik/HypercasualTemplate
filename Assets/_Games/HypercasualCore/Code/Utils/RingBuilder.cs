using UnityEngine;

// ReSharper disable UnassignedField.Global
// ReSharper disable MemberCanBePrivate.Global

[AddComponentMenu( "Builders/Ring Builder" )]
[ExecuteInEditMode]
public class RingBuilder : MonoBehaviour
{
	public Transform prefab;
	public float segmentWidth;
	public float segmentAngle;
	public float radius;
	public int steps;
	[Space]
	public bool rebuild;
	public bool loop;
	public bool clear;

	public void Update()
	{
		if(rebuild)
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

	public void Rebuild(float radius)
	{
		this.radius = radius;
		Rebuild();
	}
	
	public void Rebuild()
	{
		var len   = 2 * Mathf.PI * radius;
		steps = Mathf.FloorToInt(len / segmentWidth);

		transform.DestroyChilds();
		
		for (int i = 0; i < steps; i++)
		{
			var angle = 360f * i / steps;
			var item  = Instantiate(prefab, transform);
			item.localPosition = new Vector3(
					radius * Mathf.Cos(Mathf.Deg2Rad * angle), 0,
					radius * Mathf.Sin(Mathf.Deg2Rad * angle)
				);
			item.localRotation = Quaternion.Euler(0, -angle + segmentAngle, 0);
		}
	}
}