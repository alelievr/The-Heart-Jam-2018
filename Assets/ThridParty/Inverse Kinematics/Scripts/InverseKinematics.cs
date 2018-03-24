using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class InverseKinematics : MonoBehaviour {

	public Transform upperArm;
	public Transform forearm;
	public Transform hand;
	public Transform elbow;
	public Transform target;
	[Space(20)]
	public Vector2 uppperArm_OffsetRotation;
	public Vector2 forearm_OffsetRotation;
	public Vector2 hand_OffsetRotation;
	[Space(20)]
	public bool handMatchesTargetRotation = true;
	[Space(20)]
	public bool debug;

	float angle;
	float upperArm_Length;
	float forearm_Length;
	float arm_Length;
	float targetDistance;
	float adyacent;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void LateUpdate () {
		
	}

	void OnDrawGizmos(){
		if (debug) {
			if(upperArm != null && elbow != null && hand != null && target != null && elbow != null){
				Gizmos.color = Color.gray;
				Gizmos.DrawLine (upperArm.position, forearm.position);
				Gizmos.DrawLine (forearm.position, hand.position);
				Gizmos.color = Color.red;
				Gizmos.DrawLine (upperArm.position, target.position);
				Gizmos.color = Color.blue;
				Gizmos.DrawLine (forearm.position, elbow.position);
			}
		}
	}

}
