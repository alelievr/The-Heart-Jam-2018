using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneInitializer
{
	[RuntimeInitializeOnLoadMethod]
	static void InitializeScene()
	{
		var go = new GameObject("GameManager (RUNTIME)");

		go.AddComponent< GameManager >();
		go.AddComponent< EventTracker >();
	}

}
