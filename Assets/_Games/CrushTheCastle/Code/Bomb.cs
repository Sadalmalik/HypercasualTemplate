using System;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable UnassignedField.Global
// ReSharper disable MemberCanBePrivate.Global

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Explosive))]
public class Bomb : MonoBehaviour
{
	public Rigidbody body;
	public Explosive explosive;
	public float lifetimeLimit = 5;
	
	private bool _timer;
	private float _endTime;

	public void Update()
	{
		if (_timer && _endTime<Time.time)
		{
			_timer = false;
			explosive.Explode();
		}
	}

	
	public void Shoot()
	{
		gameObject.SetActive(true);
		_endTime = Time.time + lifetimeLimit;
		_timer = true;
	}

	void OnCollisionEnter(Collision collision)
	{
		_timer = false;
		explosive.Explode();
	}
}