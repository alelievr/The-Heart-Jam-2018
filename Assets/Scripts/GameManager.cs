using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	bool		lose = false;
	
	EventTracker tracker;

	void Awake()
	{
		DontDestroyOnLoad(this);
		Time.timeScale = 1;
		instance = this;
	}

	private void Start()
	{
		tracker = GetComponent< EventTracker >();
	}

	public void Win()
	{
		Debug.Log("Win!");
		Time.timeScale = 0;
		GUIManager.instance.ShowWinPanel();
	}

	public void Lose()
	{
		Debug.Log("LOOSE !");
		tracker.LevelFail();
		Time.timeScale = 0;
		GUIManager.instance.ShowLosePanel();
		lose = true;
	}

	public void Restart()
	{
		Debug.Log("Restart!");
		tracker.LevelFail();
		Time.timeScale = 1;
		lose = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void Quit()
	{
		tracker.LevelQuit();
		Application.Quit();
	}

	private void Update()
	{
		Debug.Log("Loose: " + lose);
		if (lose && Input.GetKeyDown(KeyCode.Space))
			Restart();
	}
}
