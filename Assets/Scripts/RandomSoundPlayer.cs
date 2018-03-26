using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{

	public float minSoundDelay = 20;
	public float delayRange = 50;
	public float volume = .2f;

	public AudioClip[]	clips;

	AudioSource	audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent< AudioSource >();

		StartCoroutine(RandomPlaySound());
	}
	
	IEnumerator RandomPlaySound()
	{
		while (true)
		{
			yield return new WaitForSeconds(minSoundDelay + Random.Range(0, delayRange));
			audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)], volume);
		}
	}
}
