using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class soundintro : MonoBehaviour {

	// Use this for initialization
	int step = 0;
	public List<AudioClip>	soundbystep;
	AudioSource				aus = null;

	void Start () {
		aus = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnEnable()
	{
		if (step == soundbystep.Count)
		{
			Time.timeScale = 0;
			SceneManager.LoadScene("DownToTheAbyssFinal");
		}
		if (aus == null)
			aus = GetComponent<AudioSource>();
		aus.PlayOneShot(soundbystep[step]);
		step++;
	}
}
