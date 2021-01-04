using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable UnassignedField.Global
// ReSharper disable MemberCanBePrivate.Global

[ExecuteInEditMode]
[AddComponentMenu("Builders/Grid Builder")]
public class GridBuilder : BaseBuilder
{
	[Space]
	[Header("<< Generation >>--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")]
	public GameObject[] prefabs;
	public int xSize;
	public int ySize;
	public int zSize;
	public Vector3 xStep = new Vector3(1, 0, 0);
	public Vector3 yStep = new Vector3(0, 1, 0);
	public Vector3 zStep = new Vector3(0, 0, 1);
	public Vector3 xAngleStep;
	public Vector3 yAngleStep;
	public Vector3 zAngleStep;

	[Space]
	[Header("<< Randomize >>--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")]
	public Vector3 randomOffset;
	public Vector3 rotationRandomOffset;

	public override void Rebuild()
	{
		for (int y = 0; y < ySize; y++)
		for (int z = 0; z < zSize; z++)
		for (int x = 0; x < xSize; x++)
		{
			var pos       = xStep * x + yStep * y + zStep * z;
			var rPosition = RandomUtils.GetRandomIn(randomOffset);
			var item      = Instantiate(prefabs.ChoiseRandom(), transform);
			item.transform.localPosition = pos + rPosition;

			var angle = xAngleStep * x + yAngleStep * y + zAngleStep * z;
			var rRot  = RandomUtils.GetRandomIn(rotationRandomOffset);
			item.transform.localRotation = Quaternion.Euler(angle + rRot);
		}
	}
}