using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedroController : MonoBehaviour {


	public float speed = 5f;
	public GameObject body;
	Rigidbody2D bodyrb;
	Rigidbody2D rb;
	WaterSurface water;

	// Use this for initialization
	void Start () {
		water = FindObjectOfType<WaterSurface>();
		rb = this.GetComponent<Rigidbody2D>();
		bodyrb = body.GetComponent<Rigidbody2D>();
	}
	
	void	MovePedro()
	{
		Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
		Vector2 mvt = dir * speed;
		if (water.playerisin)
			bodyrb.AddForce(mvt);
	}
	// Update is called once per frame
	void Update () {
		bodyrb.AddForce(new Vector2(Random.Range(-1500f, 1500f) , Random.Range(-1500f, 1500f)));
		MovePedro();
	}
}
