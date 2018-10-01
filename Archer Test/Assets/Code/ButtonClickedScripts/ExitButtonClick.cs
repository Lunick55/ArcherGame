using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonClick : MonoBehaviour {

	public void OnExitButtonClick()
	{
		EventManager.FireEvent("ExitButtonClick");
	}
}
