using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
public class WaterSurface : MonoBehaviour
{
	public float		airPercent = 50;
	public float		checkSize = 10;
	
	[Space]
	public int			perlinSamples = 20;
	public int			additionalSamples = 4;
	public float		scrollSpeed = .1f;
	public float		noiseHeightMultiplier = 1;
	
	[Space]
	public MeshFilter	meshFilter;
	new public BoxCollider2D	collider;

	Mesh			waterMesh;

	Quaternion		lastRotation;
	float			smoothLastRotation;
	float			rotationVelocity;

	int				lastPointsUnderWaterCount;

	float			waterHeight = 1;
	float			noiseHeight = .3f;

	Triangulator	triangulator;

	Buoyancy[]		buoyancers;

	LineRenderer	lineRenderer;
	Vector3[]		positions;

	RaycastHit2D[]	rightCollisions = new RaycastHit2D[2];
	RaycastHit2D[]	leftCollisions = new RaycastHit2D[2];

	private void Awake()
	{
		buoyancers = FindObjectsOfType< Buoyancy >();
		lastRotation = transform.rotation;
	}

	void Start ()
	{
		collider = GetComponent< BoxCollider2D >();
		lineRenderer = GetComponent< LineRenderer >();

		waterMesh = new Mesh();
		positions = new Vector3[perlinSamples + 2];
	}
	
	void Update ()
	{
		UpdateNoiseHeight();
		UpdateWaterHeight();
		UpdateLineRenderer();
		UpdateBuoyancers();
	}

	void UpdateNoiseHeight()
	{
	    float rotationDelta = Quaternion.Angle(transform.rotation, lastRotation);
		
		smoothLastRotation = Mathf.SmoothDamp(smoothLastRotation, rotationDelta / 4f, ref rotationVelocity, .5f);
		noiseHeight = smoothLastRotation + .1f;

		lastRotation = transform.rotation;
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

	List< Vector2 > GetPointsUnderWater()
	{
		return GetBoxVertices().Where(p => p.y < GetBoxCenter().y + waterHeight).ToList();
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
		int i;

		if (perlinSamples < 0)
			return ;

		if (rightCollisionCount == 1 && leftCollisionCount == 1)
		{
			Vector2 start = rightCollisions[0].point;
			Vector2 end = leftCollisions[0].point;

			var underWaterPoints = GetPointsUnderWater();

			if (lineRenderer.positionCount != perlinSamples || underWaterPoints.Count != lastPointsUnderWaterCount)
			{
				lineRenderer.positionCount = perlinSamples + additionalSamples;
				positions = new Vector3[perlinSamples + additionalSamples + underWaterPoints.Count];
				triangulator = null;
				lastPointsUnderWaterCount = underWaterPoints.Count;
				waterMesh.Clear();
			}
			
			float step = (start - end).magnitude / perlinSamples;
			for (i = 0; i < perlinSamples + additionalSamples; i++)
			{
				float noise = Mathf.PerlinNoise(start.x + step * i + Time.time * scrollSpeed, Time.time * scrollSpeed);
				Vector2 p = new Vector2(start.x + step * i - step *  (additionalSamples / 2), start.y + noise * noiseHeight * noiseHeightMultiplier);
				positions[i] = p;
				lineRenderer.SetPosition(i, p);
			}

			underWaterPoints.Sort((p1, p2) => p2.x.CompareTo(p1.x));
			for (i = 0; i < underWaterPoints.Count; i++)
				positions[perlinSamples + additionalSamples + i] = underWaterPoints[i];
		}

		if (triangulator == null)
			triangulator = new Triangulator(positions);

		waterMesh.vertices = positions;
		waterMesh.triangles = triangulator.Triangulate();
		waterMesh.RecalculateBounds();
		if (meshFilter != null)
			meshFilter.sharedMesh = waterMesh;
	}

	private void OnDrawGizmos()
	{
		Vector3 center = GetBoxCenter();

		if (collider == null)
			collider = GetComponent< BoxCollider2D >();
		Vector2 pos = center + Vector3.up * waterHeight - Vector3.right * checkSize;
		Gizmos.color = Color.cyan;
		Gizmos.DrawSphere(pos, .1f);

		Vector3 waterPos = new Vector3(0, waterHeight + center.y, 0);
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(waterPos, .2f);
		
		Gizmos.color = Color.blue;
		foreach (var p in GetPointsUnderWater())
		{
			Gizmos.DrawSphere(p, .3f);
		}
	}
}
