using System;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
	public int HitPoints = 1;
	public int minDamage = 1;
	
	public event Action OnDestruct;
	
	public void Hit(int damage)
	{
		if (damage<minDamage)
			return;
		HitPoints -= damage;
		if (HitPoints <= 0)
		{
			OnDestruct?.Invoke();
			Destroy(gameObject, 0);
		}
	}
}