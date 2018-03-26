using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LayerManager : MonoBehaviour
{
	public GameObject	target;
	public Gradient		cameraBackground;
	public Rigidbody2D	containerRigidbody;
	public AudioClip	clang;
	public List<AudioClip> listmusic;
	public AudioSource		audioSource;

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
			if (listmusic[currentLayer - 1] != null)
			{
				audioSource.DOFade(0, 1f).OnComplete(() => {
					audioSource.clip = listmusic[currentLayer - 1];
					audioSource.Play();
					audioSource.DOFade(1, 1);
				});
			}
			EventTracker.instance.LayerUpdate(currentLayer);
			Debug.Log("Current layer: " + currentLayer);
		}

		lastLayer = currentLayer;
	}


	void ApplyLayerRandomHit(int currentLayer)
	{
		if (Random.value < 0.001f + 0.0002f * currentLayer)
		{
			var p = postProcessingManager.ranges[0];

			switch (p.type)
			{
				case ObstacleType.Shark:
					Vector2 point = p.forcePoints[Random.Range(0, p.forcePoints.Count)];
					containerRigidbody.AddForceAtPosition((Vector2.right * point.x).normalized * 600, (Vector3)point + transform.position, ForceMode2D.Impulse);
					AudioSource.PlayClipAtPoint(clang, (Vector3)point + transform.position, 1);
					break ;

				case ObstacleType.Rock:
					// GameObject.Instantiate()
					break ;

				case ObstacleType.Cliff:

					break ;
			}
		}
	}
}
