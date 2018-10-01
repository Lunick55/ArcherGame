using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerButtonClick : MonoBehaviour {

	public void OnBlockerButtonClick()
	{
		Debug.Log("BLOCKER_CLICKED");
		EventManager.FireEvent("BlockerButtonClick");
	}
}
