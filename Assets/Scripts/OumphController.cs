﻿using System.Collections;
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
	void Update () {
		if (water.playerisin)
		{
			torse.AddForce(new Vector2(Random.Range(-200f, 200f) , Random.Range(-200f, 200f)));
			Input.GetAxisRaw("Vertical");
			torse.AddForce(new Vector2((axish) ? Input.GetAxis("Horizontal") * mvtForce : 0,
							(axisv) ?  Input.GetAxis("Vertical") * mvtForce : 0));
			axish = (Input.GetAxisRaw("Horizontal") != 0) ? false : true;
			axisv = (Input.GetAxisRaw("Vertical") != 0) ? false : true;
		}
	}
}
