using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OumphController : MonoBehaviour {


	public GameObject	arm;
	public Rigidbody2D	torse;
	public float		mvtForce;
	bool				canoumph = true;
	public float		cdoumph = 1;
	bool				axish = false;
	bool				axisv = true;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (canoumph)
		{
			Input.GetAxisRaw("Vertical");
			torse.AddForce(new Vector2((axish) ? Input.GetAxis("Horizontal") * mvtForce : 0,
							(axisv) ?  Input.GetAxis("Vertical") * mvtForce : 0));
			axish = (Input.GetAxisRaw("Horizontal") != 0) ? false : true;
			axisv = (Input.GetAxisRaw("Vertical") != 0) ? false : true;
		}
	}
}
