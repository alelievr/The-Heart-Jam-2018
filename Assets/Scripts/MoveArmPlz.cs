using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArmPlz : MonoBehaviour {

	public	HingeJoint2D		arm;
	public	GameObject			restOfTheBody;
	public	float				movePower = 100;
	public	BernardController	bController;

	private	Rigidbody2D			armRigidbody;
	private	Rigidbody2D			rotbRigidbody;
	private	bool				grabbing;

	// Use this for initialization
	void Start () {
		armRigidbody = arm.GetComponent< Rigidbody2D >();
		rotbRigidbody = restOfTheBody.GetComponent< Rigidbody2D >();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 mousePos = Input.mousePosition - Camera.main.WorldToScreenPoint(arm.transform.position);

		Vector2 mvt = new Vector2(mousePos.x / 4, mousePos.y).normalized * movePower;
		armRigidbody.AddForce(mvt);
		// Debug.Log(mvt);
		if (bController.grabbing) {
			rotbRigidbody.AddForce(-mvt * 2);
		}
	}
}
