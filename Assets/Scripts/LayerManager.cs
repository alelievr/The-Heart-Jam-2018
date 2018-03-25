using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
	public GameObject	target;
	public Gradient		cameraBackground;
	public Rigidbody2D containerRigidbody;

	BoxCollider[]		colliders;
	Layer[]				layers;

	PostProcessingManager	postProcessingManager;

	int					lastLayer = -1;

	Camera				mainCam;

	void Start ()
	{
		colliders = GetComponentsInChildren< BoxCollider >();
		layers = GetComponentsInChildren< Layer >();
		postProcessingManager = GetComponent< PostProcessingManager >();
		mainCam = Camera.main;
	}
	
	void Update ()
	{
		float y = target.transform.position.y;
		int currentLayer = -1;

		for (int i = 0; i < colliders.Length; i++)
		{
			var collider = colliders[i];
			Vector3 m = collider.bounds.center;
			m.y = y;
			if (collider.bounds.Contains(m))
			{
				currentLayer = layers[i].layer;
				break ;
			}
		}

		if (currentLayer != -1)
			ApplyLayerRandomHit(currentLayer);

		mainCam.backgroundColor = cameraBackground.Evaluate(-mainCam.transform.position.y / postProcessingManager.maxDepth);

		if (currentLayer != lastLayer)
		{
			EventTracker.instance.LayerUpdate(currentLayer);
			Debug.Log("Current layer: " + currentLayer);
		}

		lastLayer = currentLayer;
	}

	void ApplyLayerRandomHit(int currentLayer)
	{
		if (Random.value < .01f)
		{
			var p = postProcessingManager.ranges[currentLayer - 1];

			switch (p.type)
			{
				case ObstacleType.Shark:
					Vector2 point = p.forcePoints[Random.Range(0, p.forcePoints.Count)];
					Debug.Log("Shark: " + point);
					containerRigidbody.AddForceAtPosition((Vector2.right * point.x).normalized * 1000, (Vector3)point + transform.position, ForceMode2D.Impulse);
					Debug.DrawRay((Vector3)point + transform.position, Vector2.right * point.x * 10, Color.red, 1);
					break ;
			}
		}
	}
}
