using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tentacleSegment : MonoBehaviour {

    [SerializeField] private float wiggleSpeed;
    [SerializeField] private float wiggleLength;
    private float orgPosY, orgPosX;
    Rigidbody2D rb;
    Rigidbody2D parentRb;
    Animator anim;
    float timer = 0;
    bool canTime = false;
    bool canWiggle = true;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        parentRb = transform.parent.GetComponent<Rigidbody2D>();
        orgPosY = transform.localPosition.y;
        orgPosX = transform.localPosition.x;
	}

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(parentRb.velocity.x, 0);
        if (canWiggle)
        { 
            Wiggle();
        }
        if (canTime)
        {
            timer += 0.01f;
        }
	}

    void Wiggle()
    {
        transform.localPosition = new Vector3(orgPosX, orgPosY + Mathf.Sin(timer * wiggleSpeed) * wiggleLength, 0.0f);
    }

    public void StartMoving()
    {
        canTime = true;
    }

    public void StopWiggling()
    {
        canWiggle = false;
    }

    public void Die()
    {
        anim.SetBool("dead", true);
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
}
