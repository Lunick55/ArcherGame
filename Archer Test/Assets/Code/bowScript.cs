﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class bowScript : MonoBehaviour {

	int currTime, prevTime;

	public float maxVelocity;
	public float fullDrawTimeInSec;

	float fullDrawTimeInFPS;
	float drawForce;
	private float angle;
	private float trueShotTimer = 0;

	public bool aiming = true;

	Vector3 mousePos;

	public ParticleSystem ps;
	public ParticleSystem psCharging;
	public ParticleSystem psFullCharge;
	GameObject arrow;
	GameObject finalArrow;

	Animator anim;

	string arrowType = "arrow";//get rid of this later--------------------------------------------<

	// Use this for initialization
	void Start ()
   {
		prevTime = 0;
		Application.targetFrameRate = 30;

		//arrow = Resources.Load("Arrow") as GameObject;
		arrow = Resources.Load("Arrow") as GameObject;

		finalArrow = Resources.Load("FinalArrow") as GameObject;

		//EventManager.AddListener("ArrowDestroyed", Reload);
		//EventManager.AddListener("ExitButtonClick", Reload);
		EventManager.AddListener("ObjectSelected", Unload);
		EventManager.AddListener("ObjectPlaced", Reload);
		EventManager.AddListener("LoadPierceArrow", SetPierce);
		EventManager.AddListener("BaseHit", Recoil);

		fullDrawTimeInFPS = fullDrawTimeInSec * Application.targetFrameRate;

		anim = GetComponent<Animator>();

		//ps = GetComponent<ParticleSystem>();
		ps.Stop();
		psCharging.Stop();
		psFullCharge.Stop();
	}

	// Update is called once per frame
	void Update ()
    {
       mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition);
		if (aiming == true)
		{
			Rotate();
			GetInput();
		}
	}

   void Rotate()
   {
       Vector3 distance = (mousePos - gameObject.transform.position);

       angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;

       transform.eulerAngles = new Vector3(0, 0, angle);
   }

	void GetInput()
	{
		////this is so mouse clicks won't be interpreted when on top of canvas objects
		//if (EventSystem.current.IsPointerOverGameObject() == true)
		//{
		//	return;
		//}

		if (Input.GetMouseButtonDown(0))
		{
			//start timer
			trueShotTimer = 0;

        }
        else if (Input.GetMouseButton(0))
		{
            anim.SetBool("nocked", true);

            trueShotTimer += Time.deltaTime;

			if (drawForce < maxVelocity)
			{
				drawForce += maxVelocity / fullDrawTimeInFPS;

				if (ps.isPlaying == false)
					ps.Play();
			}
			else
			{
				ps.Stop();
				ChargeFinalArrow();
			}

			GetComponent<lineRenderScript>().velocity = drawForce;
			GetComponent<lineRenderScript>().angle = gameObject.transform.eulerAngles.z;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			if (trueShotTimer > 10)
			{
				Debug.Log("TRUE ARROW AWWWAAAYYY");
				FireFinalArrow();
			}
			else
			{
				FireArrow();
				Rest();
			}
		}
	}

	//TODO: eventually set this up to fire different arrow types
	void FireArrow()
	{
		if (arrowType == "arrow")
		{
			GameObject newArrow = Instantiate(arrow, transform.position, transform.rotation, null) as GameObject;
			newArrow.GetComponent<arrowScript>().SetDrawForce(drawForce);
			//drawForce = 0;
		}
		if (arrowType == "arrowPierce")
		{
			GameObject newArrow = Instantiate(arrow, transform.position, transform.rotation, null) as GameObject;

			newArrow.GetComponent<arrowPierceScript>().SetDrawForce(drawForce);
			//drawForce = 0;
			arrowType = "arrow";
			arrow = Resources.Load("Arrow") as GameObject;//remove this. Arrow types should all be preloaded, maybe in array
		}
	}

	void FireFinalArrow()
	{
		GameObject newFinalArrow = Instantiate(finalArrow, transform.position, transform.rotation, null) as GameObject;

		newFinalArrow.GetComponent<arrowScript>().SetDrawForce(drawForce);
		//aiming = false;
		drawForce = 0;
		EventManager.FireEvent("LoadWin");
	}

	void ChargeFinalArrow()
	{
		currTime = Mathf.FloorToInt(trueShotTimer);

		if (currTime > prevTime && trueShotTimer < 10)
		{
			var em = psCharging.emission;
			em.rateOverTime = em.rateOverTime.constant  + 5;

			if (psCharging.isPlaying == false)
				psCharging.Play();
		}
		else if (trueShotTimer > 10)
		{
			psCharging.Stop();

			if (psFullCharge.isPlaying == false)
				psFullCharge.Play();
		}

		prevTime = currTime;
	}

	void Reload()
	{
		Debug.Log("RELOAD");
		aiming = true;
	}

	void Unload()
	{
		aiming = false;
		Rest();
	}

	void Rest()
	{
		ps.Stop();
		psCharging.Stop();
		psFullCharge.Stop();
		var em = psCharging.emission;
		em.rateOverTime = 0;
		drawForce = 0;
		anim.SetBool("nocked", false);
	}

	void Recoil()
	{
		drawForce = 0;
		trueShotTimer = 0;
        ps.Stop();
        psCharging.Stop();
        psFullCharge.Stop();
        var em = psCharging.emission;
		em.rateOverTime = 0;
        //anim.SetBool("nocked", false);
        anim.Rebind();
    }


    void SetPierce()
	{
		arrowType = "arrowPierce";
		arrow = Resources.Load("arrowPierce") as GameObject;//remove this. Arrow types should all be preloaded, maybe in array
		Reload();
	}
}
