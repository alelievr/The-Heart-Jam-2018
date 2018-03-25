using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmphController : MonoBehaviour {


	public GameObject	arm;
	public Rigidbody2D	torse;
	public float		mvtForce;
	bool				canoumph = true;
	public float		cdoumph = 1;
	bool				isinoumph = false;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (canoumph)
		{
			if (!isinoumph)
				StartCoroutine("corouCanomph");
			torse.AddForce(new Vector2(Input.GetAxis("Horizontal") * mvtForce, Input.GetAxis("Vertical") * mvtForce));
		}
	}

	IEnumerable corouCanomph() {
		isinoumph = true;
		yield return new WaitForSeconds(0.1f);
		canoumph = false;
		yield return new WaitForSeconds(cdoumph);
		canoumph = true;
		isinoumph = false;
	}
}
