using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

	[SerializeField] GameObject audioLinger;
	[SerializeField] AudioClip deathClip;

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
		if (col.tag == "Auto")
		{
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
		rb.bodyType = RigidbodyType2D.Static;
		rb.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
		anim.SetBool("Dead", true);

		GameObject tempAudio = Instantiate(audioLinger) as GameObject;
		tempAudio.GetComponent<lingerSound>().setClip(deathClip);
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
