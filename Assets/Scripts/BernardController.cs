using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BernardController : MonoBehaviour {

	public	GameObject	Hand;

	[HideInInspector]
	public	bool		grabbing;

	private CollidingDetector	handCollider;
	private	bool	collided;
	private	GameObject	grabbed;
	private	Transform	offsetGrabbed;

	public	bool isinwater;
	// Use this for initialization
	void Start () {
		handCollider = Hand.GetComponent<CollidingDetector>();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Mouse0) && handCollider.collided) {
			grabbed = handCollider.grabbed;
			grabbing = true;
			offsetGrabbed = handCollider.grabbed.transform;
			Debug.Log("catch");
		} else if (Input.GetKeyUp(KeyCode.Mouse0)) {
			grabbing = false;
		}
		if (grabbing) {
			Hand.transform.position += grabbed.transform.position - offsetGrabbed.position;
			Hand.transform.eulerAngles +=  grabbed.transform.eulerAngles - offsetGrabbed.eulerAngles;
		}
	}
}
