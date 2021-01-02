using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PooledRingBuilder : MonoBehaviour
{
	private ObjectsPool<Transform> _pool;

	public Transform prefab;
	public float segmentWidth;
	public float segmentAngle;
	public float radius;
	public int steps;

	private readonly List<Transform> _nodes = new List<Transform>();

	void Awake()
	{
		_pool = ObjectsPoolUtils.CreateTransformsPool(() => Instantiate(prefab, transform));
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

		var count = _nodes.Count;
		if (count > steps)
		{
			// Если блоков в избытке - убираем
			for (int i = steps; i < count; i++)
				_pool.Free(_nodes[i]);
			_nodes.RemoveRange(steps, count - steps);
		}
		else if (count < steps)
		{
			// Если не хватает блоков - добавляем
			for (; count < steps; count++)
				_nodes.Add(_pool.Lock());
		}
		
		Debug.Assert(_nodes.Count==steps);
		
		for (int i = 0; i < steps; i++)
		{
			var angle = 360f * i / steps;
			var item  = _nodes[i];
			item.localPosition = new Vector3(
					radius * Mathf.Cos(Mathf.Deg2Rad * angle), 0,
					radius * Mathf.Sin(Mathf.Deg2Rad * angle)
				);
			item.localRotation = Quaternion.Euler(0, -angle + segmentAngle, 0);
		}
	}
}