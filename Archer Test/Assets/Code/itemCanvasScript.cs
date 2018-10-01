using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemCanvasScript : MonoBehaviour {

	//Create a canvas object to remove the GetComponent call

	// Use this for initialization
	void Start () 
	{
		GetComponentInChildren<Canvas>().enabled = false;
	}

	// Update is called once per frame
	void Update () 
	{

	}

	public void EnableBlockerCanvas()
	{
		GetComponentInChildren<Canvas>().enabled = true;
		//Time.timeScale = 0f;
	}
	public void DisableBlockerCanvas()
	{
		GetComponentInChildren<Canvas>().enabled = false;
		//Time.timeScale = 1.0f;
	}
}
