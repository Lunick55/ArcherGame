using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickedScript : MonoBehaviour {

	public string buttonEvent;

	public void ButtonClick()
	{
		EventManager.FireEvent(buttonEvent);
	}
}
