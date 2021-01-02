using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
	public PooledRingBuilder pooledRingBuilder;
	public TankController tankController;
	public Cannon cannon;
	[Space]
	public Bomb regularBombPrefab;
	public Bomb powerBombPrefab;
	
	[Space]
	public float radius = 30;

	public ObjectsPool<Bomb> regularBombs;
	public ObjectsPool<Bomb> powerBombs;

	void Awake()
	{
		pooledRingBuilder.Rebuild(radius);
		tankController.SetRadius(radius, pooledRingBuilder.steps);
		
		regularBombs = ObjectsPoolUtils.CreateBehavioursPool(()=>Instantiate(regularBombPrefab));
		powerBombs = ObjectsPoolUtils.CreateBehavioursPool(()=>Instantiate(powerBombPrefab));
		
		RadiusSetter(radius);
	}
	
	public Tween DoRadius(float radius, float duration)
	{
		return DOTween.To(RadiusGetter, RadiusSetter, radius, duration);
	}

	private float RadiusGetter() => radius;

	private void RadiusSetter(float newRadius)
	{
		radius = newRadius;
		pooledRingBuilder.Rebuild(radius);
		tankController.SetRadius(radius, pooledRingBuilder.steps);
	}
}