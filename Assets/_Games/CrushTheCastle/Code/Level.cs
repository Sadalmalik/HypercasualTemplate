using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class Level : MonoBehaviour
{
	public float radius;
	public float cannonBallStartVelocity;

	public Destructable[] targets;

	public int count;
	public event Action OnDestruct;

	void Awake()
	{
		count = targets.Length;
		foreach (var target in targets)
			target.OnDestruct += HandleTargetDestruction;
	}

	private void HandleTargetDestruction()
	{
		count--;
		if (count <= 0)
		{
			OnDestruct?.Invoke();
		}
	}

	public void OnDrawGizmos()
	{
		var temp = Handles.color;
		Handles.color = Color.yellow;
		Handles.CircleHandleCap(0, transform.position, Quaternion.Euler(90, 0, 0), radius, EventType.Repaint);
		Handles.color = temp;
	}

	public Tween DOMove(Vector3 position, float duration)
	{
		return transform.DOMove(position, duration);
	}
}