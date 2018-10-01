using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerManagerScript : MonoBehaviour {

	GameObject SentryBlocker;
	Vector2 mousePos;

	// Use this for initialization
	void Start () 
	{
		SentryBlocker = Resources.Load("SentryBlocker") as GameObject;

		EventManager.AddListener("SentryButtonClick", SpawnSentry);
	}

	// Update is called once per frame
	void Update () 
	{
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

	}

	void SpawnSentry()
	{
		Debug.Log("SPAWN SENTRY");

		GameObject newSentry = Instantiate(SentryBlocker) as GameObject;
		newSentry.transform.position = mousePos;
	}
}
