using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShaker : MonoBehaviour {

	public CinemachineVirtualCamera	virtualCamera;
	public bool shake;

	NoiseSettings			defaultNoise;
	float					defaultfrequency;
	float					defaultAmplitude;

	public NoiseSettings	shakeSettings;

	CinemachineBasicMultiChannelPerlin noise;

	// Use this for initialization
	void Start () {
		noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin> ();
		defaultNoise = noise.m_NoiseProfile;
		defaultAmplitude = noise.m_AmplitudeGain;
		defaultfrequency = noise.m_FrequencyGain;
	}

	public void Shake(float force = 4, float time = .4f)
	{
		StartCoroutine(StartShake(force, time));
		shake = false;
	}

    IEnumerator StartShake(float shakeIntensity, float shakeTiming)
    {
		noise.m_NoiseProfile = shakeSettings;
		noise.m_AmplitudeGain = .7f;
		noise.m_FrequencyGain = shakeIntensity;
        yield return new WaitForSeconds(shakeTiming);
		noise.m_NoiseProfile = defaultNoise;
		noise.m_AmplitudeGain = defaultAmplitude;
		noise.m_FrequencyGain = defaultfrequency;
    }

    // Update is called once per frame
    void Update () {
		if (shake)
			Shake();
	}
}
