using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WaterSurface : MonoBehaviour
{
	public float		airPercent = 50;
	public float		checkSize = 10;
	new BoxCollider2D	collider;
	
	[Space]
	public int			perlinSamples = 20;
	public float		scrollSpeed = .1f;
	public float		noiseHeight = .3f;

	float			waterHeight = 1;

	Buoyancy[]		buoyancers;

	LineRenderer	lineRenderer;

	RaycastHit2D[]	rightCollisions = new RaycastHit2D[2];
	RaycastHit2D[]	leftCollisions = new RaycastHit2D[2];

	private void Awake()
	{
		buoyancers = FindObjectsOfType< Buoyancy >();
	}

	void Start ()
	{
		collider = GetComponent< BoxCollider2D >();
		lineRenderer = GetComponent< LineRenderer >();
	}
	
	void Update ()
	{
		UpdateWaterHeight();
		UpdateLineRenderer();
		UpdateBuoyancers();
	}

	void UpdateWaterHeight()
	{
		float c = airPercent / 100f;
		float min = 1e20f, max = -1e20f;

		foreach (var p in GetBoxVertices())
		{
			min = Mathf.Min(min, p.y);
			max = Mathf.Max(max, p.y);
		}

		float ab = min - max;

		float fillHeight = (c * ab) - (ab / 2);

		waterHeight = fillHeight;
	}

	void UpdateBuoyancers()
	{
		Vector2 center = GetBoxCenter();

		foreach (var buoyancer in buoyancers)
		{
			buoyancer.waterLevel = waterHeight + center.y;
		}
	}

	IEnumerable< Vector2 > GetBoxVertices()
	{
		Vector2 s = collider.size / 2;
		Vector2 o = collider.offset;
		yield return transform.TransformPoint(o + s);
		yield return transform.TransformPoint(o - s);
		yield return transform.TransformPoint(o + new Vector2(-s.x, s.y));
		yield return transform.TransformPoint(o + new Vector2(s.x, -s.y));
	}

	Vector3 GetBoxCenter()
	{
		return transform.TransformPoint(collider.offset);
	}

	void UpdateLineRenderer()
	{
		Vector3 center = GetBoxCenter();
		Vector2 rightPos = center + Vector3.up * waterHeight - Vector3.right * checkSize;
		Vector2 leftPos = center + Vector3.up * waterHeight - Vector3.left * checkSize;
		int mask = 1 << LayerMask.NameToLayer("ContainerWall");
		int rightCollisionCount = Physics2D.RaycastNonAlloc(rightPos, Vector2.right, rightCollisions, checkSize * 2, mask);
		int leftCollisionCount = Physics2D.RaycastNonAlloc(leftPos, Vector2.left, leftCollisions, checkSize * 2, mask);

		if (perlinSamples < 0)
			return ;

		if (rightCollisionCount == 1 && leftCollisionCount == 1)
		{
			Vector2 start = rightCollisions[0].point;
			Vector2 end = leftCollisions[0].point;

			if (lineRenderer.positionCount != perlinSamples)
				lineRenderer.positionCount = perlinSamples + 2;
			
			float step = (start - end).magnitude / perlinSamples;
			for (int i = 0; i < perlinSamples + 2; i++)
			{
				float noise = Mathf.PerlinNoise(start.x + step * i + Time.time * scrollSpeed, start.y + Time.time * scrollSpeed);
				Vector2 p = new Vector2(start.x + step * i - step, start.y + noise * noiseHeight);
				lineRenderer.SetPosition(i, p);
			}
		}
	}

	private void OnDrawGizmos()
	{
		Vector3 center = GetBoxCenter();

		if (collider == null)
			collider = GetComponent< BoxCollider2D >();
		Vector2 pos = center + Vector3.up * waterHeight - Vector3.right * checkSize;
		Gizmos.DrawSphere(pos, .1f);

		Vector3 waterPos = new Vector3(0, waterHeight + center.y, 0);
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere(waterPos, .2f);

		Gizmos.color = Color.green;
		foreach (var p in GetBoxVertices())
		{
			Gizmos.DrawSphere(p, .2f);
		}
	}
}
