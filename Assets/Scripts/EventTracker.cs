using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class EventTracker : MonoBehaviour
{
	public static EventTracker instance;

	void Awake()
	{
		instance = this;

		LevelStart();
	}

	void LevelStart()
	{
		Debug.Log("Level start !");
		string sceneName = SceneManager.GetActiveScene().name;
		// AnalyticsEvent.LevelStart(sceneName);
	}

	public void LayerUpdate(int layer)
	{
		// AnalyticsEvent.LevelUp(layer);
	}

	public void LevelFail()
	{
		Debug.Log("Level fail !");
		string sceneName = SceneManager.GetActiveScene().name;
		// AnalyticsEvent.LevelFail(sceneName);
	}
	
	public void LevelComplete()
	{
		Debug.Log("Level Completed !");
		string sceneName = SceneManager.GetActiveScene().name;
		// AnalyticsEvent.LevelComplete(sceneName);
	}
	
	public void LevelQuit()
	{
		Debug.Log("Level Quit !");
		string sceneName = SceneManager.GetActiveScene().name;
		// AnalyticsEvent.LevelQuit(sceneName);
	}
}
