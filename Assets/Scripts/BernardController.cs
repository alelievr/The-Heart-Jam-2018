using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BernardController : MonoBehaviour {


	public GameObject arm;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);
		Vector3 worldUp = transform.TransformDirection(Vector3.up);
		Quaternion rotation = Quaternion.LookRotation(mouseWorldPosition  - transform.position, worldUp);
		// transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
		Debug.Log(mouseWorldPosition  - transform.position);
		arm.transform.position = mouseWorldPosition  - transform.position;
	}
}
