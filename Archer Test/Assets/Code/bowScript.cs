using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class bowScript : MonoBehaviour 
{
	public float maxVelocity;
	public float fullDrawTimeInSec;

	float fullDrawTimeInFPS;
	float drawForce;
	private float angle;

	public bool aiming = true;

	Vector3 mousePos;

	public ParticleSystem ps;
	GameObject arrow;
	GameObject finalArrow;

	Animator anim;

    bool endGame = false;

	string arrowType = "arrow";//get rid of this later--------------------------------------------<

	// Use this for initialization
	void Start ()
   {
		Application.targetFrameRate = 30;

		arrow = Resources.Load("Arrow") as GameObject;

		finalArrow = Resources.Load("FinalArrow") as GameObject;

		EventManager.AddListener("ObjectSelected", Unload);
		EventManager.AddListener("ObjectPlaced", Reload);
		EventManager.AddListener("LoadPierceArrow", SetPierce);

		fullDrawTimeInFPS = fullDrawTimeInSec * Application.targetFrameRate;

		anim = GetComponent<Animator>();

		ps.Stop();
	}

	// Update is called once per frame
	void Update ()
    {
       mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition);
		if (aiming == true && endGame == false)
		{
			Rotate();
			GetInput();
		}
      if (endGame)
      {
         EventManager.FireEvent("GAMEOVER");
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

      }
      else if (Input.GetMouseButton(0))
		{
            anim.SetBool("nocked", true);

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
		else if (Input.GetMouseButtonUp(0))
		{
			FireArrow();
			Rest();
		
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
		aiming = false;
		drawForce = 0;
      //EventManager.FireEvent("LoadWin");
      endGame = true;
      WorldManager.GoToEnd();
	}

	void Reload()
	{
		Debug.Log("RELOAD");
		aiming = true;
	}

	void Unload()
	{
		Debug.Log("UNLOAD");
		aiming = false;
		Rest();
	}

	//Used during pause screens to fully reset the bow, as well as after firing
	void Rest()
	{
		ps.Stop();
		drawForce = 0;
		anim.SetBool("nocked", false);
	}

   void SetPierce()
	{
		arrowType = "arrowPierce";
		arrow = Resources.Load("arrowPierce") as GameObject;//remove this. Arrow types should all be preloaded, maybe in array
		Reload();
	}
}
