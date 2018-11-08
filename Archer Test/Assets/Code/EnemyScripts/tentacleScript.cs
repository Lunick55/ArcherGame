using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tentacleScript : MonoBehaviour
{

	Rigidbody2D rb;
	Renderer rend;

	// Use this for initialization
	void Start()
	{
		rend = GetComponent<Renderer>();
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = new Vector2(-2, 0);

		//clean this up. maybe set it in the spawner script
		transform.position = new Vector2(transform.position.x + 9, transform.position.y);
		transform.eulerAngles = new Vector3(0, 0, 90);
	}

	// Update is called once per frame
	void Update()
	{
		if (rend.isVisible == false)
		{
			DestroyEnemy();
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Arrow")
		{
			EventManager.FireEvent("SmallFill");
			EventManager.FireEvent("DestroyArrow");
			DestroyEnemy();
		}
		if (col.tag == "Barrier")
		{
			rb.velocity = new Vector2(0, 0);
		}
		if (col.tag == "Base")
		{
			rb.velocity = new Vector2(0, 0);
			EventManager.FireEvent("BaseHit");
			CameraManager.ShakeCamera();
		}
	}

	private void OnTriggerStay2D(Collider2D col)
	{
		if (col.tag == "Barrier")
		{
			col.GetComponent<barrierScript>().BarrierStrength--;

			//rb.velocity = new Vector2(0, 0);
			//DestroyEnemy();
		}
		else if (col.tag == "Base")
		{
			col.GetComponent<BossAndBaseHealth>().DamageWallSlow(1);
		}
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Barrier")
		{
			rb.velocity = new Vector2(-2, 0);
			//DestroyEnemy();
		}
	}

	void DestroyEnemy()
	{
		//Don't destroy. Weaken until dead. Multiple hits to kill
		Destroy(gameObject);
	}
}
