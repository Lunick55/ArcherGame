﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SentryButtonClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

	public void OnSentryClick()
	{
		Debug.Log("SENTRY BUTTON CLICK");
		EventManager.FireEvent("SentryButtonClick");
	}

	public void OnPointerEnter(PointerEventData data)
	{
		EventManager.FireEvent("ViewCostMed");
	}

	public void OnPointerExit(PointerEventData data)
	{
		EventManager.FireEvent("ViewNoCost");
	}
}
