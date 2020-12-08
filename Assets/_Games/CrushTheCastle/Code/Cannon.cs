using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
	public Transform ancor;
	public Transform markerPrefab;
	public int markersCount = 30;
	public float timeOffset;
	public float timeStep;
	public float startVelocity;
	[Space]
	public Rigidbody cannonball;
	public bool shoot;

	private List<Transform> _markers;

	void Start()
	{
		_markers = new List<Transform>();
		for (int i = 0; i < markersCount; i++)
		{
			_markers.Add(Instantiate(markerPrefab, ancor));
		}
	}

	void Update()
	{
		RebuildTrajectory();
		
		if (shoot)
		{
			shoot = false;
			Shoot();
		}
	}
	
	private void Shoot()
	{
		cannonball.position = ancor.position;
		cannonball.velocity = ancor.forward * startVelocity;
	}
	
	private void RebuildTrajectory()
	{
		// Уравнение движения тела например
		// https://www.fxyz.ru/%D1%84%D0%BE%D1%80%D0%BC%D1%83%D0%BB%D1%8B_%D0%BF%D0%BE_%D1%84%D0%B8%D0%B7%D0%B8%D0%BA%D0%B5/%D0%BC%D0%B5%D1%85%D0%B0%D0%BD%D0%B8%D0%BA%D0%B0/%D0%BA%D0%B8%D0%BD%D0%B5%D0%BC%D0%B0%D1%82%D0%B8%D0%BA%D0%B0/%D0%BF%D0%B0%D0%B4%D0%B5%D0%BD%D0%B8%D0%B5_%D1%82%D0%B5%D0%BB/%D0%B4%D0%B2%D0%B8%D0%B6%D0%B5%D0%BD%D0%B8%D0%B5_%D1%82%D0%B5%D0%BB%D0%B0_%D0%B1%D1%80%D0%BE%D1%88%D0%B5%D0%BD%D0%BD%D0%BE%D0%B3%D0%BE_%D0%BF%D0%BE%D0%B4_%D1%83%D0%B3%D0%BB%D0%BE%D0%BC_%D0%BA_%D0%B3%D0%BE%D1%80%D0%B8%D0%B7%D0%BE%D0%BD%D1%82%D1%83/%D1%83%D1%80%D0%B0%D0%B2%D0%BD%D0%B5%D0%BD%D0%B8%D0%B5_%D0%B4%D0%B2%D0%B8%D0%B6%D0%B5%D0%BD%D0%B8%D1%8F_%D1%82%D0%B5%D0%BB%D0%B0_%D0%B1%D1%80%D0%BE%D1%88%D0%B5%D0%BD%D0%BD%D0%BE%D0%B3%D0%BE_%D0%BF%D0%BE%D0%B4_%D1%83%D0%B3%D0%BB%D0%BE%D0%BC_%D0%BA_%D0%B3%D0%BE%D1%80%D0%B8%D0%B7%D0%BE%D0%BD%D1%82%D1%83/
		var start = ancor.position;
		var velocity = ancor.forward * startVelocity;
		var vertical = Vector3.up * velocity.y;
		velocity.y = 0;

		for (int i = 0; i < markersCount; i++)
		{
			var time = timeStep * i + timeOffset;
			_markers[i].position = start +
				velocity * time +
				vertical * time +
				Physics.gravity * time * time * 0.5f;
			if (i>0)
				_markers[i-1].LookAt(_markers[i]);
		}
		_markers[markersCount-1].LookAt(_markers[markersCount-2]);
	}
}