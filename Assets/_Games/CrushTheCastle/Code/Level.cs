using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class Level : MonoBehaviour
{
	public float radius;
	public float cannonBallStartVelocity;
	public int[] bombs;
	
	public Destructable[] targets;

	public int _count;
	public event Action OnDestruct;

	void Awake()
	{
		_count = targets.Length;
		foreach (var target in targets)
			target.OnDestruct += HandleTargetDestruction;
	}

	private void HandleTargetDestruction()
	{
		_count--;
		if (_count <= 0)
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