using UnityEngine;

// ReSharper disable UnassignedField.Global
// ReSharper disable MemberCanBePrivate.Global

public class BaseBuilder : MonoBehaviour
{
	[Header("<< Control >>--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")]
	public bool generate;
	public bool loop;
	public bool clear;
	
	public void Awake()
	{
		if (Application.isPlaying)
		{
			generate = false;
			loop = false;
			clear = false;
			enabled = false;
		}
	}

	public void Update()
	{
		if(generate && !clear)
		{
			generate = loop;
			transform.DestroyChilds();
			Rebuild();
		}
		
		if (clear)
		{
			generate = false;
			loop = false;
			clear = false;
			transform.DestroyChilds();
		}
	}
	
	public virtual void Rebuild()
	{
		
	}
}
