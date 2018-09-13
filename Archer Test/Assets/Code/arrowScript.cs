using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowScript : MonoBehaviour {

	float drawForce;
	Rigidbody2D rb;

	int lifeTime;

	// Use this for initialization
	void Start () 
	{
		lifeTime = 0;		

	}

	// Update is called once per frame
	void Update () 
	{
		lifeTime++;
		if (lifeTime > 120)
		{
			Destroy(gameObject);
		}	
	}

	private void FixedUpdate()
	{
		Vector3 dir = rb.velocity;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	public void SetDrawForce(float newDrawForce)
	{
		rb = gameObject.GetComponent<Rigidbody2D>();
		rb.velocity = transform.right * newDrawForce;
	}
}
