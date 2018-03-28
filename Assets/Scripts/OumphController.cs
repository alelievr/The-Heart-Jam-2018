using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OumphController : MonoBehaviour {


	public GameObject	arm;
	public Rigidbody2D	torse;
	public float		mvtForce;
	bool				axish = false;
	bool				axisv = true;
	WaterSurface		water;
	// Use this for initialization
	void Start () {
		water = FindObjectOfType<WaterSurface>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (water.playerisin)
		{
			torse.AddForce(new Vector2(Random.Range(-300f, 300f) , Random.Range(-300f, 300f)));
			Input.GetAxisRaw("Vertical");
			torse.AddForce(new Vector2((axish) ? Input.GetAxisRaw("Horizontal") * mvtForce : 0,
							(axisv) ?  Input.GetAxisRaw("Vertical") * mvtForce : 0));
			axish = (Input.GetAxisRaw("Horizontal") != 0) ? false : true;
			axisv = (Input.GetAxisRaw("Vertical") != 0) ? false : true;
		}
	}
}
