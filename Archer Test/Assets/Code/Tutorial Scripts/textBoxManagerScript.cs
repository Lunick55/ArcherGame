using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textBoxManagerScript : MonoBehaviour {

	private static textBoxManagerScript textManager;

	[SerializeField] GameObject[] textBoxes;
	int currLine = 0;
	public int currBox = 0;

	[SerializeField] GameObject nextButton;
	[SerializeField] GameObject enemySpawner;
	[SerializeField] GameObject panelBackground;

	public static textBoxManagerScript instance
	{
		get
		{
			if (!textManager)
			{
				textManager = FindObjectOfType(typeof(textBoxManagerScript)) as textBoxManagerScript;

				textManager.Init();
			}

			return textManager;
		}
	}

	void Init()
	{
		EventManager.AddListener("NextLine", NextLine);
		EventManager.AddListener("NextBox", NextBox);

		instance.textBoxes[currBox].SetActive(true);
		instance.textBoxes[currBox].transform.GetChild(currLine).gameObject.SetActive(true);
	}

	// Use this for initialization
	void Start () 
	{
		EventManager.AddListener("NextLine", NextLine);
		EventManager.AddListener("NextBox", NextBox);

		textBoxes[currBox].SetActive(true);
		textBoxes[currBox].transform.GetChild(currLine).gameObject.SetActive(true);
	}

	// Update is called once per frame
	void Update () 
	{
		
	}

	void NextLine()
	{

		if (currLine < textBoxes[currBox].transform.childCount-1)
		{
			textBoxes[currBox].transform.GetChild(currLine).gameObject.SetActive(false);
			currLine++;

			textBoxes[currBox].transform.GetChild(currLine).gameObject.SetActive(true);
		}
		else 
		{
			panelBackground.SetActive(false);
			textBoxes[currBox].SetActive(false);
			nextButton.SetActive(false);
			if (currBox == 1)
			{
				enemySpawner.SetActive(true);
			}
			if (currBox == 2)
			{
				WorldManager.tutAbilityActive = true;
			}
			if (currBox == 2)
			{
				WorldManager.tutEndSoon = true;
			}
			currBox++;
		}
	}

	void NextBox()
	{
		currLine = 0;
		panelBackground.SetActive(true);
		textBoxes[currBox].SetActive(true);
		textBoxes[currBox].transform.GetChild(currLine).gameObject.SetActive(true);
		nextButton.SetActive(true);
	}
}
