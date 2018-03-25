using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public class OumphController : MonoBehaviour {
=======
public class OmphController : MonoBehaviour {
>>>>>>> 5b2877c2f94019268334964a00119681d901ed71


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
