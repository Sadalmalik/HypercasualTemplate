using System.Text;
using UnityEngine;

// ReSharper disable UnassignedField.Global
// ReSharper disable MemberCanBePrivate.Global

[ExecuteInEditMode]
public class SphereBuilder : MonoBehaviour
{
	public GameObject[] prefabs;
	public int count;
	public float radius;
	public float aspect;
	public float minHAngle;
	public float maxHAngle;
	public Vector2 randomOffsets;
	public Vector3 center;
	public Vector3 additional;
	public bool generate;
	public bool loop;

	void Start()
	{
		enabled = false;
	}

	void Update()
	{
		if (generate)
		{
			generate = loop;
			Rebuild();
		}
	}

	public void Rebuild()
	{
		transform.DestroyChilds();

		for (int i = 0; i < count; i++)
		{
			var item = Instantiate(RandomUtils.Choice(prefabs), transform);

			var offset = i / (float) count;
			var vAngle = Mathf.Deg2Rad * (
				360 * offset * aspect +
				2 * (Random.value - 0.5f) * randomOffsets.y);
			var hAngle = Mathf.Deg2Rad * (
				Mathf.Lerp(minHAngle, maxHAngle, offset) +
				2 * (Random.value - 0.5f) * randomOffsets.x);

			var ver = Mathf.Cos(hAngle);
			var pos = new Vector3(ver * Mathf.Cos(vAngle), Mathf.Sin(hAngle), ver * Mathf.Sin(vAngle));

			item.transform.localPosition = radius * pos;
			item.transform.LookAt(transform.position + center);
			item.transform.localRotation = item.transform.localRotation * Quaternion.Euler(additional);
		}
	}
}