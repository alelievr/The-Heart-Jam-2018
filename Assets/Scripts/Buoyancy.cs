using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyancy : MonoBehaviour
{

	[HideInInspector]
	public float waterLevel = 0;

	public float floatThreshold = 2;
	public float waterDensity = .125f;
	public float downForce = 5;

	Rigidbody2D rbody;

	float forceFactor;
	Vector3 floatForce;

	void Start ()
	{
		rbody = GetComponent< Rigidbody2D >();
	}
	
	private void FixedUpdate()
	{
		forceFactor = 1 - ((transform.position.y - waterLevel) / floatThreshold);

		if (forceFactor > 0)
		{
			floatForce = -Physics.gravity * (forceFactor - rbody.velocity.y * waterDensity);
			floatForce += new Vector3(0, -downForce * rbody.mass, 0);
			rbody.AddForceAtPosition(floatForce, transform.position);
		}
	}
}
