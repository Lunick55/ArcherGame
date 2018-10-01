using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

	Rigidbody2D rb;
	Renderer rend;

	// Use this for initialization
	void Start () 
	{
		rend = GetComponent<Renderer>();
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = new Vector2(-6, 0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (rend.isVisible == false)
		{
			DestroyEnemy();
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Arrow")
		{
			EventManager.FireEvent("DestroyArrow");
			DestroyEnemy();
		}
		if (col.tag == "Sentry")
		{
			col.GetComponent<sentryScript>().SentryStrength--;
			DestroyEnemy();
		}
	}

	void DestroyEnemy()
	{
		Destroy(gameObject);
	}
}
