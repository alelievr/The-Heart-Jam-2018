using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public enum ObstacleType
{
	Rock,
	Shark,
	Cliff,
}

[System.Serializable]
public struct PostProcessRange
{
	public float			range;
	public ObstacleType		type;
	public List< Vector3 >	forcePoints;

	public PostProcessRange(float range = 10, ObstacleType type = ObstacleType.Cliff, List< Vector3 > points = null)
	{
		this.range = range;
		this.type = type;
		this.forcePoints = new List< Vector3 >();
	}
	
}

[ExecuteInEditMode]
public class PostProcessingManager : MonoBehaviour
{
	public float heightOffset = 10;

	[Space]
	public float blendRange = 1.5f;
	public float width = 40;

	[Space]
	public List< PostProcessRange >	ranges = new List< PostProcessRange >();

	[HideInInspector]
	public float maxDepth;

	Camera				mainCam;

	BoxCollider[]		postProcessColliders;
	PostProcessVolume[]	postProcessVolumes;

	void Start ()
	{
		mainCam = Camera.main;
		postProcessColliders = GetComponentsInChildren< BoxCollider >();
		postProcessVolumes = GetComponentsInChildren< PostProcessVolume >();
	}
	
	void Update ()
	{
		while (ranges.Count < postProcessColliders.Length)
			ranges.Add(new PostProcessRange());
		
		float startY = heightOffset;
		for (int i = 0; i < postProcessColliders.Length; i++)
		{
			var c = postProcessColliders[i];
			var p = postProcessVolumes[i];
			
			if (c == null || p == null)
				continue ;
			
			float range = ranges[i].range;
			var size = c.size;
			var pos = transform.position;

			size.y = range;
			size.x = width;
			pos.y = -startY - range / 2;
			c.transform.position = pos;

			postProcessVolumes[i].blendDistance = blendRange;

			c.size = size;
			startY += range;
		}

		maxDepth = startY;
	}
}
