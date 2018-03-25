using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PostProcessingManager))]
public class PostPorcessLayerEditor : Editor {

	PostProcessingManager	manager;

	void OnEnable () {
		manager = target as PostProcessingManager;
	}
	
	void 	OnSceneGUI()
	{
		foreach (var r in manager.ranges)
		{
			for (int i = 0; i < r.forcePoints.Count; i++)
				r.forcePoints[i] = Handles.PositionHandle(r.forcePoints[i], Quaternion.identity);
		}
	}

}
