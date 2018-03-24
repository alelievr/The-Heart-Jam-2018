using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ShaderControle : MonoBehaviour {

	// Use this for initialization
	public float		depth;
	public float		oxygen;
	public float		speedtmp = 1;
	PostProcessVolume	volume;
	PreDeathEffect			predeatheffect;
	public float		distortionIntensity;

	void OnEnable () {
		volume = GetComponent<PostProcessVolume>();		
	}
	
	// Update is called once per frame
	void Update () {
		oxygenchange();
		oxygen += Time.deltaTime * speedtmp;
		// cheapDistorsion();
	}

    void oxygenchange ()
	{
		if(volume.profile == null)
		{
			enabled = false;
			Debug.Log("Cant load PostProcess volume");
			return;
		}
        bool foundEffectSettings = volume.profile.TryGetSettings<PreDeathEffect>(out predeatheffect);
		if(!foundEffectSettings)
		{
			enabled = false;
			Debug.Log("Cant load PreDeathEffect settings");
			return;
		}
		predeatheffect.Strenght.Override((oxygen > 50) ? (oxygen - 50) / 110 : 0);
		predeatheffect.greytougoum.Override((oxygen > 50) ? (oxygen > 75) ? 0.4f : 0.2f : 0);
		predeatheffect.greystrenght.Override((oxygen > 60) ? (oxygen - 60) / 40 : 0);
    }

	void cheapDistorsion()
	{
		LensDistortion ld;
		bool foundEffectSettings = volume.profile.TryGetSettings<LensDistortion>(out ld);
		if (!foundEffectSettings)
		{
			enabled = false;
			Debug.Log("Cant load LensDistortion settings");
			return;
		}
		ld.centerX.Override(Mathf.Sin(Time.timeSinceLevelLoad * 100 * Input.GetAxis("Horizontal")));
		ld.centerY.Override(Mathf.Sin(Time.timeSinceLevelLoad * 100 * Input.GetAxis("Horizontal")));
	}
}
