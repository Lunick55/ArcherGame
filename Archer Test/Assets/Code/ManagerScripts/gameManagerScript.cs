using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManagerScript : MonoBehaviour {

	public GameObject BlockerCanvas;
	itemCanvasScript canvasScript;

	// Use this for initialization
	void Start () 
	{
		canvasScript = BlockerCanvas.GetComponent<itemCanvasScript>();

		EventManager.AddListener("BlockerButtonClick", OpenBlockerMenu);
		EventManager.AddListener("SentryButtonClick", ExitBlockerMenuPause);
		EventManager.AddListener("ExitButtonClick", ExitBlockerMenuResume);
		EventManager.AddListener("ObjectPlaced", Resume);
	}

	// Update is called once per frame
	void Update () 
	{

	}

	void OpenBlockerMenu()
	{
		Pause();
		canvasScript.EnableBlockerCanvas();
	}

	void ExitBlockerMenuPause()
	{
		Pause();
		canvasScript.DisableBlockerCanvas();
	}

	void ExitBlockerMenuResume()
	{
		Resume();
		canvasScript.DisableBlockerCanvas();
	}

	void Pause()
	{
		Time.timeScale = 0.0f;
	}
	void Resume()
	{
		Time.timeScale = 1.0f;
	}
}
