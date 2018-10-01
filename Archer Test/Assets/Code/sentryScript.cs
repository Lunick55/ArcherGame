using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sentryScript : MonoBehaviour {

	Vector2 mousePos;
	bool activated = false;
	public int SentryStrength;

	// Use this for initialization
	void Start () 
	{
		//EventManager.AddListener("WeakenSentry", WeakenSentry);
	}
	
	// Update is called once per frame
	void Update () 
	{
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		if (Input.GetMouseButtonUp(0) == true && activated == false)
		{
			activated = true;
			EventManager.FireEvent("ObjectPlaced");
		}

		if (activated == false)
		{
			//follow mouse
			transform.position = mousePos;
		}
		else
		{
			SentryMode();
		}

		if (SentryStrength <= 0)
		{
			Destroy(gameObject);
		}
	}

	void SentryMode()
	{
		this.tag = "Sentry";
	}
}
