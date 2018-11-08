using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tentacleScript : MonoBehaviour
{
    GameObject[] segments;

	Rigidbody2D rb;

    [SerializeField] private float tentacleSpeed = 0;

	// Use this for initialization
	void Start()
	{
        segments = new GameObject[transform.childCount];
        for (int i = 0; i < segments.Length; i++)
        {
            segments[i] = transform.GetChild(i).gameObject;
            segments[i].GetComponent<tentacleSegment>().Invoke("StartMoving", (float)(i/2.0f));
        }

		rb = GetComponent<Rigidbody2D>();
		rb.velocity = new Vector2(tentacleSpeed, 0);

		//clean this up. maybe set it in the spawner script
		transform.position = new Vector2(transform.position.x + 2, transform.position.y);
		//transform.eulerAngles = new Vector3(0, 0, 90);
	}

	// Update is called once per frame
	void Update()
	{
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Arrow")
		{
            rb.velocity = new Vector2(0, 0);
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
			rb.velocity = new Vector2(0, 0);
			//DestroyEnemy();
		}
		else if (col.tag == "Base")
		{
            Debug.Log("BASE VAMPIRE");
            col.GetComponent<BossAndBaseHealth>().DamageWallSlow(1);
		}
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Barrier")
		{
			rb.velocity = new Vector2(tentacleSpeed, 0);
			//DestroyEnemy();
		}
	}

	void DestroyEnemy()
	{
        //Don't destroy. Weaken until dead. Multiple hits to kill
        GetComponent<Collider2D>().enabled = false;

        for (int i = 0; i < segments.Length; i++)
        {
            segments[i].GetComponent<tentacleSegment>().Invoke("StopWiggling", 0.0f);
            segments[i].GetComponent<tentacleSegment>().Invoke("Die", (float)(i / 4.0f));
        }

        //Destroy(gameObject);
	}
}
