using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class sentryScript : MonoBehaviour {

	Vector2 mousePos;
	bool activated = false;
	bool canPlace = true;
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

		GetInput();

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

	void GetInput()
	{
		if (EventSystem.current.IsPointerOverGameObject() == true)
		{
			return;
		}

		if (Input.GetMouseButtonUp(0) == true && activated == false && canPlace)
		{
			activated = true;
			EventManager.FireEvent("ObjectPlaced");
		}
	}

	void SentryMode()
	{
		this.tag = "Sentry";
	}

	private void OnTriggerStay2D(Collider2D col)
	{
		canPlace = false;
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("IM IN");
		canPlace = false;
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		Debug.Log("IM OUT");
		canPlace = true;
	}
}
