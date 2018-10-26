using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PierceButtonClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

	public void OnPierceClick()
	{
		Debug.Log("PIERCE BUTTON CLICK");
		EventManager.FireEvent("PierceButtonClick");
	}

	public void OnPointerEnter(PointerEventData data)
	{
		EventManager.FireEvent("ViewCostHigh");
	}

	public void OnPointerExit(PointerEventData data)
	{
		EventManager.FireEvent("ViewNoCost");
	}
}
