using System;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable UnassignedField.Global
// ReSharper disable MemberCanBePrivate.Global

[Serializable]
public class DamageRange
{
	public string tag;
	public float radius;
	public int hitPower;
}

public class Explosive : MonoBehaviour
{
	public DamageRange[] damages;
    
	public event Action OnExplode;
	
	public void OnDrawGizmos()
	{
		var pos = transform.position;
		foreach (var damage in damages)
			Gizmos.DrawWireSphere(pos, damage.radius);
	}
	
	public void Explode()
	{
		var hitted = new HashSet<Destructable>();
		var pos = transform.position;
		
		foreach (var damage in damages)
		{
			var colliders = Physics.OverlapSphere(pos, damage.radius);

			foreach (var collider in colliders)
			{
				var dest = collider.GetComponent<Destructable>();
				if (dest!=null && !hitted.Contains(dest))
				{
					hitted.Add(dest);
					dest.Hit(damage.hitPower);
				}
			}
		}
		
		gameObject.SetActive(false);
		
		OnExplode?.Invoke();
	}
}
