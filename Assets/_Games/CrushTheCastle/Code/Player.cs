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
	public Transform cameraAnchor;
	[Space]
	public float radius = 30;

	// [Space]
	// public Bomb regularBombPrefab;
	// public Bomb powerBombPrefab;
	
	// public ObjectsPool<Bomb> regularBombs;
	// public ObjectsPool<Bomb> powerBombs;

	// void Awake()
	// {
	// }
	
	void Start()
	{
		RadiusSetter(radius);
	}
	
	public Tween DORadius(float radius, float duration)
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
	
    public void Move(Vector2 amount)
    {
	    tankController.Move(amount);
    }
    
    public void Shoot(Bomb bomb)
    {
		cannon.Shoot(bomb.body);
		bomb.Shoot();
    }
    
    public void SetShootVelocity(float velocity)
    {
	    cannon.startVelocity = velocity;
    }
}