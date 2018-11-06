using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockerManagerScript : MonoBehaviour {

	public GameObject BlockerBar;
	private currencyBarScript myBlockerBar;
	public int sentryCost = 0;
	public int barrierCost = 0;

	GameObject SentryBlocker;
	GameObject BarrierBlocker;

	Vector2 mousePos;

	// Use this for initialization
	void Start () 
	{
		SentryBlocker = Resources.Load("SentryBlocker") as GameObject;
		BarrierBlocker = Resources.Load("BarrierBlocker") as GameObject;

		if (BlockerBar != null)
		{
			myBlockerBar = BlockerBar.GetComponent<currencyBarScript>();
		}
	}

	// Update is called once per frame
	void Update () 
	{
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

	}

	public bool SpawnBlocker(int type)
	{
		switch (type)
		{
			case 1:
				if (myBlockerBar.MedCost() == true)
				{
					GameObject newSentry = Instantiate(SentryBlocker) as GameObject;
					newSentry.transform.position = mousePos;
					return true;
				}
				break;
			case 2:
				if (myBlockerBar.SmallCost() == true)
				{
					GameObject newBarrier = Instantiate(BarrierBlocker) as GameObject;
					newBarrier.transform.position = mousePos;
					return true;
				}
				break;
			case 3:
				if (myBlockerBar.HighCost() == true)
				{
					Debug.Log("PIERCE ARROW");
					EventManager.FireEvent("LoadPierceArrow");
					return true;
				}
				break;
			default:
				return false;
		}
		return false;
	}
}