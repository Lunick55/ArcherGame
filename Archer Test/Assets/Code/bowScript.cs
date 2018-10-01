using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class bowScript : MonoBehaviour {

	public float maxVelocity;
	public float fullDrawTimeInSec;

	float fullDrawTimeInFPS;
	float drawForce;
	private float angle;

	bool aiming = true;

	Vector3 mousePos;

	ParticleSystem ps;
	GameObject arrow;

	// Use this for initialization
	void Start ()
   {
		Application.targetFrameRate = 60;

		arrow = Resources.Load("Arrow") as GameObject;

		EventManager.AddListener("BlockerButtonClick", Rest);
		EventManager.AddListener("ArrowDestroyed", Reload);
		EventManager.AddListener("ExitButtonClick", Reload);
		EventManager.AddListener("ObjectPlaced", Reload);

		fullDrawTimeInFPS = fullDrawTimeInSec * Application.targetFrameRate;

		ps = GetComponent<ParticleSystem>();
		ps.Stop();
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

		if (Input.GetMouseButton(0))
		{
			if (drawForce < maxVelocity)
			{
				drawForce += maxVelocity / fullDrawTimeInFPS;

				if (ps.isPlaying == false)
					ps.Play();
			}
			else
			{
				ps.Stop();
			}

			GetComponent<lineRenderScript>().velocity = drawForce;
			GetComponent<lineRenderScript>().angle = gameObject.transform.eulerAngles.z;
		}
		if (Input.GetMouseButtonUp(0))
		{
			ps.Stop();

			FireArrow();
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	//TODO: eventually set this up to fire different arrow types
	void FireArrow()
	{
		GameObject newArrow = Instantiate(arrow,transform.position, transform.rotation, null) as GameObject;
		//GameObject newArrow = Instantiate(arrow, transform) as GameObject;

		//newArrow.transform.SetPositionAndRotation(transform.position, transform.rotation);

		newArrow.GetComponent<arrowScript>().SetDrawForce(drawForce);
		aiming = false;
		drawForce = 0;
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
}
