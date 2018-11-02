using System.Collections;
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

	bool aiming = true;

	Vector3 mousePos;

	public ParticleSystem ps;
	public ParticleSystem psCharging;
	public ParticleSystem psFullCharge;
	GameObject arrow;
	GameObject finalArrow;

	string arrowType = "arrow";//get rid of this later--------------------------------------------<

	// Use this for initialization
	void Start ()
   {
		prevTime = 0;
		Application.targetFrameRate = 60;

		//arrow = Resources.Load("Arrow") as GameObject;
		arrow = Resources.Load("Arrow") as GameObject;

		finalArrow = Resources.Load("FinalArrow") as GameObject;

		EventManager.AddListener("BlockerButtonClick", Rest);
		EventManager.AddListener("ArrowDestroyed", Reload);
		EventManager.AddListener("ExitButtonClick", Reload);
		EventManager.AddListener("LoadPierceArrow", SetPierce);
		EventManager.AddListener("ObjectPlaced", Reload);
		EventManager.AddListener("BaseHit", Recoil);

		fullDrawTimeInFPS = fullDrawTimeInSec * Application.targetFrameRate;

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
		if (EventSystem.current.IsPointerOverGameObject() == true)
		{
			return;
		}

		if (Input.GetMouseButtonDown(0))
		{
			//start timer
			trueShotTimer = 0;
		}
		if (Input.GetMouseButton(0))
		{
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
		if (Input.GetMouseButtonUp(0))
		{
			if (trueShotTimer > 10)
			{
				Debug.Log("TRUE ARROW AWWWAAAYYY");
				FireFinalArrow();
			}
			else
			{
				ps.Stop();
				psCharging.Stop();
				psFullCharge.Stop();
				var em = psCharging.emission;
				em.rateOverTime = 0;
				FireArrow();
			}
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	//TODO: eventually set this up to fire different arrow types
	void FireArrow()
	{
		if (arrowType == "arrow")
		{
			GameObject newArrow = Instantiate(arrow, transform.position, transform.rotation, null) as GameObject;

			newArrow.GetComponent<arrowScript>().SetDrawForce(drawForce);
			//aiming = false;
			drawForce = 0;
		}
		if (arrowType == "arrowPierce")
		{
			GameObject newArrow = Instantiate(arrow, transform.position, transform.rotation, null) as GameObject;

			newArrow.GetComponent<arrowPierceScript>().SetDrawForce(drawForce);
			aiming = false;
			drawForce = 0;
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

	void Rest()
	{
		Debug.Log("REST");
		aiming = false;
	}

	void Recoil()
	{
		Debug.Log("Recoil");
		drawForce = 0;
		trueShotTimer = 0;
		var em = psCharging.emission;
		em.rateOverTime = 0;
	}

	void SetPierce()
	{
		arrowType = "arrowPierce";
		arrow = Resources.Load("arrowPierce") as GameObject;//remove this. Arrow types should all be preloaded, maybe in array
		Reload();
		Debug.Log("PIERCE LOADED CAPTAIN");
	}
}
