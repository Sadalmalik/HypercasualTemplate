using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Destructable))]
[RequireComponent(typeof(Explosive))]
public class Barrel : MonoBehaviour
{
	public Explosive explosive;
	public Destructable destructable;
	public float explosionDelay = 0.4f;
	
    void Start()
    {
		destructable.destroy = false;
        destructable.OnDestruct += Explode;
    }

    void Explode()
    {
		StartCoroutine(DoExplode());
    }
    
    private IEnumerator DoExplode()
    {
	    yield return new WaitForSeconds(explosionDelay);
        explosive.Explode();
	    Destroy(gameObject, 0);
    }
}
