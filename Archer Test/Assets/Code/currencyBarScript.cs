using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currencyBarScript : MonoBehaviour {

	public Image progressBar;
	public Image progressBarEarly;
	//public Image progressBarCost;
	private float personalTimer;
	public float progress = 0;
	private const float MAXFILL = 1.0f;

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
		EventManager.AddListener("ViewCostHigh", ViewCostHigh);
		EventManager.AddListener("ViewNoCost", ViewNoCost);
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
			progressBarEarly.color = new Color32(255, 0, 0, (byte)Mathf.Abs((255 * Mathf.Cos(timer))));
		}
	}

	void SmallFill()
	{
		if(progressBarEarly.fillAmount < 1.0)
			personalTimer = 0;

		canCatchUp = true;
		progressBarEarly.color = new Color32(0, 255, 0, 255);
		progressBarEarly.fillAmount = progress += 0.1f;
	}

	void MedFill()
	{
		if (progressBarEarly.fillAmount < 1.0)
			personalTimer = 0;

		canCatchUp = true;
		progressBarEarly.color = new Color32(0, 255, 0, 255);
		progressBarEarly.fillAmount = progress += 0.2f;

	}

	void BigFill()
	{
		if (progressBarEarly.fillAmount < 1.0)
			personalTimer = 0;

		canCatchUp = true;
		progressBarEarly.fillAmount = progressBar.fillAmount;
		progressBarEarly.fillAmount += 0.5f;
	}

	//TODO: turn these into a single function that take a float Cost
	public bool SmallCost()
	{
		if (fToI(progress) >= fToI(0.3f))
		{
			//FullCatchUp();
			canCatchDown = true;
			canCatchUp = false;
			progressBar.fillAmount = progress -= 0.3f;
			return true;
		}

		return false;
	}
	public bool MedCost()
	{
		if (fToI(progress) >= fToI(0.5f))
		{
			FullCatchUp();
			canCatchDown = true;
			canCatchUp = false;
			progressBar.fillAmount = progress -= 0.5f;
			return true;
		}

		return false;
	}
	public bool HighCost()
	{
		if (fToI(progress) >= fToI(0.8f))
		{
			FullCatchUp();
			canCatchDown = true;
			progressBar.fillAmount = progress -= 0.8f;
			return true;
		}

		return false;
	}

	void CatchUp()
	{
		if (progress > 1.0f)
			progress = 1.0f;

		//progress catches up to green bar
		if (fToI(progressBar.fillAmount) != fToI(progressBarEarly.fillAmount) && canCatchUp == true)
		{
			canCatchDown = false;

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
		if (fToI(progressBarEarly.fillAmount) != fToI(progressBar.fillAmount) && canCatchDown == true)
		{
			progressBarEarly.color = new Color32(255, 0, 0, 255);
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
		if (fToI(progress) >= fToI(0.3f))
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
		if (fToI(progress) >= fToI(0.5f))
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

	void ViewCostHigh()
	{
		//if you have enough
		if (fToI(progress) >= fToI(0.8f))
		{
			progressBarEarly.color = new Color32(255, 0, 0, 255);
			progressBar.fillAmount -= 0.8f;
		}
		else
		{
			progressBarEarly.fillAmount = 0.8f;
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



	int fToI(float fNum)
	{
		int iNum = 0;

		iNum = (int)Mathf.Round(MAXFILL) * (int)Mathf.Round(fNum * 100);

		return iNum;
	}
}
