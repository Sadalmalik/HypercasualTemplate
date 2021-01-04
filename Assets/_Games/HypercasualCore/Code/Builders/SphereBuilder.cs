using System.Text;
using UnityEngine;

// ReSharper disable UnassignedField.Global
// ReSharper disable MemberCanBePrivate.Global

[ExecuteInEditMode]
[AddComponentMenu( "Builders/Sphere Builder" )]
public class SphereBuilder : BaseBuilder
{
	[Space]
	[Header("<< Generation >>--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")]
	public GameObject[] prefabs;
	public int count;
	public float radius;
	public float aspect;
	public float minHAngle;
	public float maxHAngle;
	public Vector3 center;
	public Vector3 additionalAngle;
	[Space]
	[Header("<< Randomize >>--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")]
	public Vector3 randomOffset;
	public Vector2 angleRandomOffset;
	public Vector3 rotationRandomOffset;

	public override void Rebuild()
	{
		for (int i = 0; i < count; i++)
		{
			var item = Instantiate(prefabs.ChoiseRandom(), transform);

			var offset = i / (float) count;
			var rAngle = RandomUtils.GetRandomIn(angleRandomOffset);
			var vAngle = Mathf.Deg2Rad * (360 * offset * aspect + rAngle.y);
			var hAngle = Mathf.Deg2Rad * (Mathf.Lerp(minHAngle, maxHAngle, offset) + rAngle.x);

			var ver = Mathf.Cos(hAngle);
			var pos = new Vector3(ver * Mathf.Cos(vAngle), Mathf.Sin(hAngle), ver * Mathf.Sin(vAngle));
			pos += RandomUtils.GetRandomIn(randomOffset);

			item.transform.localPosition = radius * pos;
			item.transform.LookAt(transform.position + center);
			var rRot = RandomUtils.GetRandomIn(rotationRandomOffset);
			item.transform.localRotation = item.localRotation * Quaternion.Euler(additionalAngle + rRot);
		}
	}
}