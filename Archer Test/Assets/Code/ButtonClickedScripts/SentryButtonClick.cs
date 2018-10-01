using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryButtonClick : MonoBehaviour {

	public void OnSentryClick()
	{
		Debug.Log("SENTRY BUTTON CLICK");
		EventManager.FireEvent("SentryButtonClick");
	}
}
