using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArmPlz : MonoBehaviour {

	public HingeJoint2D		arm;
	public float			movePower = 100;

	Rigidbody2D				armRigidbody;

	// Use this for initialization
	void Start () {
		armRigidbody = arm.GetComponent< Rigidbody2D >();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 mousePos = Input.mousePosition - Camera.main.WorldToScreenPoint(arm.transform.position);

		armRigidbody.AddForce(new Vector2(mousePos.x / 4, mousePos.y).normalized * movePower);
	}
}
