using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		if (aus == null)
			aus = GetComponent<AudioSource>();
		aus.PlayOneShot(soundbystep[step]);
		step++;
	}
}
