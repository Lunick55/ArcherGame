using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

	Rigidbody2D rb;
	Renderer rend;
	Animator anim;
	float speed = -6.0f;

	// Use this for initialization
	void Start () 
	{
		rend = GetComponent<Renderer>();
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = new Vector2(speed, 0);

		anim = GetComponent<Animator>();
        //anim.speed = 0.3f;

        EventManager.AddListener("GAMEOVER", RemoveEnemy);
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
			EventManager.FireEvent("MedFill");
			EventManager.FireEvent("DestroyArrow");
			DestroyEnemy();
		}
		if (col.tag == "Sentry")
		{
			col.GetComponent<sentryScript>().DamageDone();
			DestroyEnemy();
		}
		if (col.tag == "Base")
		{
			col.GetComponent<BossAndBaseHealth>().DamageWall(1);
			DestroyEnemy();
			CameraManager.ShakeCamera();
		}
	}

	void DestroyEnemy()
	{
		//rb.isKinematic = false;
		rb.bodyType = RigidbodyType2D.Static;
		rb.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
		anim.SetBool("Dead", true);
		//anim.speed = 4;
		//Destroy(gameObject);
	}

	public void ChangeSpeed(float newSpeed)
	{
		speed = newSpeed;
	}

	public void RemoveEnemy()
	{
		Destroy(gameObject);
	}
}
