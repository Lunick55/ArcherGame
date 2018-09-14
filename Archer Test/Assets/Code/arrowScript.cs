using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowScript : MonoBehaviour {

	float drawForce;
	Rigidbody2D rb;

	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{
		if (gameObject.GetComponent<Renderer>().isVisible == false)
		{
			Destroy(gameObject);
			EventManager.FireEvent("Arrow Gone");
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
