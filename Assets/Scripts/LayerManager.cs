using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
	public GameObject	target;
	public Gradient		cameraBackground;

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

		mainCam.backgroundColor = cameraBackground.Evaluate(-mainCam.transform.position.y / postProcessingManager.maxDepth);

		if (currentLayer != lastLayer)
		{
			EventTracker.instance.LayerUpdate(currentLayer);
			Debug.Log("Current layer: " + currentLayer);
		}

		lastLayer = currentLayer;
	}
}
