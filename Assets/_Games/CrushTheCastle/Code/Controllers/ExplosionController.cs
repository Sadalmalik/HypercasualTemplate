using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public ParticleSystem particles;
    public float duration;
    
    public event Action OnComplete;
    
    public void Explode(Vector3 position)
    {
        transform.position = position;
        transform.gameObject.SetActive(true);
        // particles.Clear();
        // particles.Play();
        StartCoroutine(LateInvoke());
    }
    
    IEnumerator LateInvoke()
    {
        yield return new WaitForSeconds(duration);
        OnComplete?.Invoke();
    }
}
