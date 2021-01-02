using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
	public int HitPoints = 1;

	public void Hit(int damage)
	{
		HitPoints -= damage;
		if (HitPoints <= 0)
			Destroy(gameObject);
	}
}