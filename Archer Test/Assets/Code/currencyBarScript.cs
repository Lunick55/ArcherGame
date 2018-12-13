using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currencyBarScript : MonoBehaviour {

	[SerializeField] private int autoCost;
	[SerializeField] private int sentryCost;
   [SerializeField] private int barrierCost;
	[SerializeField] private int energyPoints;
	[SerializeField] private int physicalPoints;

	public Image progressBar;
	public Image progressBarEarly;
	private float personalTimer;
	public int progress = 0;
	private const float MAXFILL = 1.0f;

	private bool canCatchUp = false;
	private bool canCatchDown = false;

	// Use this for initialization
	void Start () 
	{
		progressBar.fillAmount = 0;
		progressBarEarly.fillAmount = 0;
		EventManager.AddListener("SmallFill", SmallFill);
		EventManager.AddListener("MedFill", MedFill);
		EventManager.AddListener("ObjectPlaced", FullCatchDown);
	}

	// Update is called once per frame
	void Update () 
	{
		CatchUp();
		CatchDown();

        if (fToI(progress) >= fToI(sentryCost))
        {
            WorldManager.ActivateUIElement(1);
        }
        else
        {
            WorldManager.DeactivateUIElement(1);
        }

        if (fToI(progress) >= fToI(barrierCost))
        {
            WorldManager.ActivateUIElement(0);
        }
        else
        {
            WorldManager.DeactivateUIElement(0);
        }
    }

    //TODO: Turn these into a single function
	void SmallFill()
	{
		if(progressBarEarly.fillAmount < 100)
			personalTimer = 0;

		canCatchUp = true;
		progressBarEarly.color = new Color32(0, 255, 0, 255);
		progressBarEarly.fillAmount = iToF(progress += physicalPoints);
	}
	void MedFill()
	{
		if (progressBarEarly.fillAmount < 100)
			personalTimer = 0;

		canCatchUp = true;
		progressBarEarly.color = new Color32(0, 255, 0, 255);
		progressBarEarly.fillAmount = iToF(progress += energyPoints);
	}

	//TODO: turn these into a single function that take a float Cost
	public bool TinyCost()
	{
		if (progress >= autoCost)
		{
			FullCatchUp();
			canCatchDown = true;
			canCatchUp = false;
			progressBar.fillAmount = iToF(progress -= autoCost);
			return true;
		}

		return false;
	}
	public bool SmallCost()
	{
		if (progress >= barrierCost)
		{
			FullCatchUp();
			canCatchDown = true;
			canCatchUp = false;
			progressBar.fillAmount = iToF(progress -= barrierCost);
			return true;
		}

		return false;
	}
	public bool MedCost()
	{
		if (progress >= sentryCost)
		{
			FullCatchUp();
			canCatchDown = true;
			canCatchUp = false;
			progressBar.fillAmount = iToF(progress -= sentryCost);
			return true;
		}

		return false;
	}

	void CatchUp()
	{
		if (progress > 100)
			progress = 100;

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
		progressBar.fillAmount = iToF(progress);
	}

	void FullCatchDown()
	{
		progressBarEarly.fillAmount = iToF(progress);
	}

	int fToI(float fNum)
	{
		int iNum = 0;

		iNum = (int)Mathf.Round(MAXFILL) * (int)Mathf.Round(fNum * 100);

		return iNum;
	}

	//Brings ints from 0-100 to a float from 0-1
	float iToF(int iNum)
	{
		float fNum = 0;

		fNum = (float)(iNum / 100.0f);

		return fNum;
	}
}
