using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class gameManagerScript : MonoBehaviour {

	public GameObject BlockerManager;
	BlockerManagerScript myBlockerManager;

	private static gameManagerScript gameManager;

	public static gameManagerScript instance
	{
		get
		{
			if (!gameManager)
			{
				gameManager = FindObjectOfType(typeof(gameManagerScript)) as gameManagerScript;

				gameManager.Init();
			}

			return gameManager;
		}
	}

	void Init()
	{

	}

	// Use this for initialization
	void Start()
	{
		if (BlockerManager != null)
		{
			myBlockerManager = BlockerManager.GetComponent<BlockerManagerScript>();
		}

		EventManager.AddListener("StartButtonClick", StartGame);
		EventManager.AddListener("QuitButtonClick", QuitGame);
		EventManager.AddListener("LoadMainMenu", LoadMenu);
		EventManager.AddListener("LoadWin", LoadWin);
		EventManager.AddListener("LoadLose", LoadLose);

		EventManager.AddListener("ObjectPlaced", Resume);
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			ExitBlockerPlaceState(2);//barrier
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			ExitBlockerPlaceState(1);//sentry
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	private static void ExitBlockerPlaceState(int blockType)
	{
		if (instance.myBlockerManager.SpawnBlocker(blockType) == true)
		{
			instance.Pause();
			EventManager.FireEvent("ObjectSelected");
		}
	}

	private static void ExitPierceBlockerMenuResume()
	{
		if (instance.myBlockerManager.SpawnBlocker(3) == true)
		{
			instance.Resume();
		}
	}

	void StartGame()
	{
		Debug.Log("START GAME");
		SceneManager.LoadScene("MainGameScene");
	}

	void QuitGame()
	{
		Application.Quit();
	}

	void LoadMenu()
	{
		SceneManager.LoadScene("MenuScene");
	}
	void LoadWin()
	{
		SceneManager.LoadScene("WinScene");
	}
	void LoadLose()
	{
		SceneManager.LoadScene("LoseScene");
	}

	void Pause()
	{
		Time.timeScale = 0.1f;
		Time.fixedDeltaTime = 0.02f * Time.timeScale;
	}
	void Resume()
	{
		Time.timeScale = 1.0f;
		//enable the blocker button
		Time.fixedDeltaTime = 0.02f * Time.timeScale;
	}

}
