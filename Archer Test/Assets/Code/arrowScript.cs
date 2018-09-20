using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowScript : MonoBehaviour {

	float drawForce;
	Rigidbody2D rb;
	Renderer rend;

	int lifeTime;

	// Use this for initialization
	void Start () 
	{
		rend = GetComponent<Renderer>();
		lifeTime = 0;	

		EventManager.AddListener("DestroyArrow", DestroyArrow);	
	}

	// Update is called once per frame
	void Update () 
	{
		lifeTime++;
		if (rend.isVisible == false)
		{
			EventManager.FireEvent("ArrowDestroyed");
			DestroyArrow();
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

	void DestroyArrow()
	{
		Destroy(gameObject);
	}
}
