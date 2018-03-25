using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gauchedroite : MonoBehaviour {

	// Use this for initialization
	public Transform center;
	public List<Vector2> listcible;
	public float force;
	public float cd;
	int	ciblenb = 0;
	float timesincelastcd = 0;
	Rigidbody2D rgb;
	

	void Start () {
		rgb = GetComponent<Rigidbody2D>();
		int i = -1;
		while (++i < listcible.Count)
		{
			listcible[i] += (Vector2)transform.position;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 realcible = listcible[ciblenb] + new Vector2(center.position.x, 0);
		if (Vector2.Distance(realcible, transform.position) < 0.2f)
		{
			ciblenb = (ciblenb + 1 < listcible.Count) ? ciblenb + 1 : 0;
			transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
		if (timesincelastcd > cd)
		{
			rgb.AddForce((realcible - (Vector2)transform.position).normalized * force);
			timesincelastcd = 0;
		}
		timesincelastcd += Time.deltaTime;
	}
}
