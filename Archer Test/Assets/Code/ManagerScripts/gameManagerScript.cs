using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class gameManagerScript : MonoBehaviour {

	public GameObject BlockerManager;
	BlockerManagerScript myBlockerManager;
	public GameObject BlockerCanvas;
	itemCanvasScript canvasScript;
	public Button blockerButton;

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
		if (BlockerCanvas != null)
			canvasScript = BlockerCanvas.GetComponent<itemCanvasScript>();

		if (BlockerManager != null)
		{
			myBlockerManager = BlockerManager.GetComponent<BlockerManagerScript>();
		}

		EventManager.AddListener("BlockerButtonClick", OpenBlockerMenu);
		EventManager.AddListener("SentryButtonClick", ExitSentryBlockerMenuPause);
		EventManager.AddListener("BarrierButtonClick", ExitBarrierBlockerMenuPause);
		EventManager.AddListener("PierceButtonClick", ExitPierceBlockerMenuResume);
		EventManager.AddListener("ExitButtonClick", ExitBlockerMenuResume);
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

	}

	void OpenBlockerMenu()
	{
		Pause();
		blockerButton.interactable = !blockerButton.interactable;
		canvasScript.EnableBlockerCanvas();
	}

	void ExitSentryBlockerMenuPause()
	{
		if (myBlockerManager.SpawnBlocker(1) == true)
		{
			Pause();
			canvasScript.DisableBlockerCanvas();
		}
	}
	void ExitBarrierBlockerMenuPause()
	{
		if (myBlockerManager.SpawnBlocker(2) == true)
		{
			Pause();
			canvasScript.DisableBlockerCanvas();
		}
	}
	void ExitPierceBlockerMenuResume()
	{
		if (myBlockerManager.SpawnBlocker(3) == true)
		{
			Resume();
			canvasScript.DisableBlockerCanvas();
		}
	}

	void ExitBlockerMenuResume()
	{
		Resume();
		canvasScript.DisableBlockerCanvas();
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
		blockerButton.interactable = !blockerButton.interactable;
		Time.fixedDeltaTime = 0.02f * Time.timeScale;
	}

}
