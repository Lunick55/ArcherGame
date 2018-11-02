using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sentryAnimOffset : MonoBehaviour {

	Animator anim;

	[SerializeField]
	float x;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();

		anim.SetFloat("Offset", x);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void TopDeck()
	{
		gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
	}
}
