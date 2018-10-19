﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class barrierScript : MonoBehaviour {

	SpriteRenderer rend;

	Vector2 mousePos;

	bool activated = false;
	bool canPlace = true;
	public int BarrierStrength;
	private int damage;


	// Use this for initialization
	void Start()
	{
		rend = GetComponent<SpriteRenderer>();

		//EventManager.AddListener("WeakenSentry", WeakenSentry);
	}

	// Update is called once per frame
	void Update()
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
			BarrierMode();
		}

		if (BarrierStrength <= 0)
		{
			Destroy(gameObject);
		}

		damage = (255 * (BarrierStrength * 100 / 200) / 100);

		rend.color = new Color32(255, (byte)damage, (byte)damage, 255);
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

	void BarrierMode()
	{
		this.tag = "Barrier";
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