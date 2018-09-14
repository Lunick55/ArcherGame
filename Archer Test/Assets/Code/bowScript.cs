﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowScript : MonoBehaviour {

	public float maxVelocity;
	public float fullDrawTimeInSec;
	float fullDrawTimeInFPS;

   Vector3 mousePos;
   private float angle;
	bool aiming = true;
	GameObject arrow;
	float drawForce;

	// Use this for initialization
	void Start ()
   {
		Application.targetFrameRate = 60;

		arrow = Resources.Load("Arrow") as GameObject;
		fullDrawTimeInFPS = fullDrawTimeInSec * Application.targetFrameRate;

		EventManager.AddListener("Arrow Gone", ReloadArrow);
	}

	// Update is called once per frame
	void Update ()
    {
       mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition);

		if (aiming == true)
		{
			Rotate();
			if (Input.GetMouseButton(0))
			{
				if (drawForce < maxVelocity)
					drawForce += maxVelocity / fullDrawTimeInFPS;

				GetComponent<lineRenderScript>().velocity = drawForce;
				GetComponent<lineRenderScript>().angle = gameObject.transform.eulerAngles.z;
			}
			if (Input.GetMouseButtonUp(0))
			{
				GameObject newArrow = Instantiate(arrow) as GameObject;
				newArrow.transform.position = transform.position;
				newArrow.transform.rotation = transform.rotation;

				newArrow.GetComponent<arrowScript>().SetDrawForce(drawForce);
				drawForce = 0;
				aiming = false;
			}
		}
	}

    void Rotate()
    {
        Vector3 distance = (mousePos - gameObject.transform.position);

        angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, angle);
    }

	void ReloadArrow()
	{
		aiming = true;
	}
}
