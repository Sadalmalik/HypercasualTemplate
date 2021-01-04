using System;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable UnassignedField.Global
// ReSharper disable MemberCanBePrivate.Global

[Serializable]
public class RingSetup
{
	public float radius;
	[Range(0f,360f)]
	public float rotationOffset;
	public float verticalOffset;
	public float segmentWidth;
	public Vector3 segmentRotations;
}

[ExecuteInEditMode]
[AddComponentMenu( "Builders/Ring Builder" )]
public class RingBuilder : BaseBuilder
{
	[Space]
	[Header("<< Generation >>--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")]
	public GameObject[] prefabs;
	public List<RingSetup> setups = new List<RingSetup>{ new RingSetup() };
	[Space]
	[Header("<< Randomize >>--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")]
	public Vector3 randomOffset;
	public float angleRandomOffset;
	public Vector3 rotationRandomOffset;
	
	public override void Rebuild()
	{
		foreach (var setup in setups)
		{
			var len   = 2 * Mathf.PI * setup.radius;
			var steps = Mathf.FloorToInt(len / setup.segmentWidth);
			
			for (int i = 0; i < steps; i++)
			{
				var angle = 360f * i / steps + setup.rotationOffset;
				var item  = Instantiate(prefabs.ChoiseRandom(), transform);
				var rAngle = RandomUtils.GetRandomIn(angleRandomOffset);
				var rPosition = RandomUtils.GetRandomIn(randomOffset);
				item.transform.localPosition = new Vector3(
						setup.radius * Mathf.Cos(Mathf.Deg2Rad * (angle + rAngle)),
						setup.verticalOffset,
						setup.radius * Mathf.Sin(Mathf.Deg2Rad * (angle + rAngle))
					) + rPosition;
				var rRot = RandomUtils.GetRandomIn(rotationRandomOffset);
				item.transform.localRotation = Quaternion.Euler(Vector3.down * angle + setup.segmentRotations + rRot);
			}
		}
	}
}