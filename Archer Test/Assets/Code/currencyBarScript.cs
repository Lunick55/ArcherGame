using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currencyBarScript : MonoBehaviour {

	public Image progressBar;
	public Image progressBarEarly;
	//public Image progressBarCost;
	private float personalTimer;
	private float progress = 0;

	private bool canCatchUp = false;
	private bool canCatchDown = false;
	private bool canFlash = false;
	private float timer = 0;

	// Use this for initialization
	void Start () 
	{
		progressBar.fillAmount = 0;
		progressBarEarly.fillAmount = 0;
		//progressBarCost.fillAmount = 0;
		EventManager.AddListener("SmallFill", SmallFill);
		EventManager.AddListener("MedFill", MedFill);
		EventManager.AddListener("BigFill", BigFill);
		EventManager.AddListener("ViewCostSmall", ViewCostSmall);
		EventManager.AddListener("ViewCostMed", ViewCostMed);
		EventManager.AddListener("ViewNoCost", ViewNoCost);
		EventManager.AddListener("BlockerButtonClick", FullCatchUp);
		EventManager.AddListener("ObjectPlaced", FullCatchDown);
	}

	// Update is called once per frame
	void Update () 
	{
		CatchUp();
		CatchDown();
		FlashBar();
	}

	void FlashBar()
	{
		if (canFlash == true)
		{
			timer += 0.02f;
			Debug.Log(255 * Mathf.Cos(timer));
			progressBarEarly.color = new Color32(255, 0, 0, (byte)Mathf.Abs((255 * Mathf.Cos(timer))));
		}
	}

	void SmallFill()
	{
		personalTimer = 0;
		canCatchUp = true;
		progressBarEarly.color = new Color32(0, 255, 0, 255);
		progressBarEarly.fillAmount = progress += 0.1f;
	}

	void MedFill()
	{
		personalTimer = 0;
		canCatchUp = true;
		progressBarEarly.color = new Color32(0, 255, 0, 255);
		progressBarEarly.fillAmount = progress += 0.2f;

	}

	void BigFill()
	{
		personalTimer = 0;
		canCatchUp = true;
		progressBarEarly.fillAmount = progressBar.fillAmount;
		progressBarEarly.fillAmount += 0.5f;
	}

	//TODO: turn these into a single function that take a float Cost
	public bool SmallCost()
	{
		if (progress >= 0.3f)
		{
			canCatchDown = true;
			progressBar.fillAmount = progress -= 0.3f;
			Debug.Log(progress);
			return true;
		}

		return false;
	}

	public bool MedCost()
	{
		if (progress >= 0.5f)
		{
			canCatchDown = true;
			progressBar.fillAmount = progress -= 0.5f;
			Debug.Log(progress);
			return true;
		}

		return false;
	}

	void CatchUp()
	{
		if (progress > 1.0f)
			progress = 1.0f;

		//progress catches up to green bar
		if (System.Math.Round(progressBar.fillAmount, 2) != System.Math.Round(progressBarEarly.fillAmount, 2) && canCatchUp == true)
		{
			if (personalTimer >= 1)
			{
				progressBar.fillAmount = (float)System.Math.Round(progressBar.fillAmount + 0.01f, 2);
				return;
			}
			else
			{
				personalTimer += Time.deltaTime;
				return;
			}
		}

		canCatchUp = false;
	}

	void CatchDown()
	{
		if (System.Math.Round(progressBarEarly.fillAmount, 2) != System.Math.Round(progressBar.fillAmount, 2) && canCatchDown == true)
		{
			progressBarEarly.fillAmount = (float)System.Math.Round(progressBarEarly.fillAmount - 0.01f, 2);
			return;
		}
		else if (progressBar.fillAmount > progressBarEarly.fillAmount)
		{
			progressBarEarly.fillAmount = progressBar.fillAmount;
			return;
		}

		canCatchDown = false;
	}

	void FullCatchUp()
	{
		progressBar.fillAmount = (float)System.Math.Round(progress, 2);
	}

	void FullCatchDown()
	{
		progressBarEarly.fillAmount = (float)System.Math.Round(progress, 2);
	}

	void ViewCostSmall()
	{
		if (progress >= 0.3f)
		{
			progressBarEarly.color = new Color32(255, 0, 0, 255);
			progressBar.fillAmount -= 0.3f;
		}
		else 
		{
			progressBarEarly.fillAmount = 0.3f;
			canFlash = true;
		}
	}

	void ViewCostMed()
	{
		//if you have enough
		if (progress >= 0.5f)
		{
			progressBarEarly.color = new Color32(255, 0, 0, 255);
			progressBar.fillAmount -= 0.5f;
		}
		else
		{
			progressBarEarly.fillAmount = 0.5f;
			canFlash = true;
		}
	}

	void ViewNoCost()
	{
		progressBar.fillAmount = progress;
		if (canFlash)
		{
			progressBarEarly.fillAmount = progress;
			canFlash = false;
		}
		timer = 0;
	}
}
