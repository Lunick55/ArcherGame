using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BarrierButtonClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

	public void OnBarrierClick()
	{
		Debug.Log("BARRIER BUTTON CLICK");
		EventManager.FireEvent("BarrierButtonClick");
	}

	public void OnPointerEnter(PointerEventData data)
	{
		EventManager.FireEvent("ViewCostSmall");
	}

	public void OnPointerExit(PointerEventData data)
	{
		EventManager.FireEvent("ViewNoCost");
	}
}
