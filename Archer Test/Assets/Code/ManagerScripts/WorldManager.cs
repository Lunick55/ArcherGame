using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour {

	public GameObject BlockerManager;
	BlockerManagerScript myBlockerManager;

	private int arrowBits = 0; //number to win game

	private static WorldManager worldManager;

   [SerializeField] Image[] elementUIImages;
	[SerializeField] GameObject[] elementUIPulses;

	[SerializeField] Color32 normalColor;
	[SerializeField] Color32[] abilityColors;

	public static WorldManager instance
	{
		get
		{
			if (!worldManager)
			{
				worldManager = FindObjectOfType(typeof(WorldManager)) as WorldManager;

				worldManager.Init();
			}

			return worldManager;
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
        EventManager.AddListener("LoadTutorial", LoadTutorial);
        EventManager.AddListener("LoadTutorial2", LoadTutorial2);

        EventManager.AddListener("ObjectPlaced", Resume);

         //elementUIImages = new Image[3];
	}

	// Update is called once per frame
	void Update () 
	{
		if (BlockerManager != null)
		{
			if (Time.timeScale != 0.1f)
			{
				if (Input.GetKeyDown(KeyCode.Alpha1))
				{
					ExitBlockerPlaceState(3);//pierce
				}
				if (Input.GetKeyDown(KeyCode.Alpha3))
				{
					ExitBlockerPlaceState(2);//barrier
				}
				if (Input.GetKeyDown(KeyCode.Alpha2))
				{
					ExitBlockerPlaceState(1);//sentry
				}
			}
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                LoadMenu();
            }
		}

		if (instance.arrowBits > 5)
		{
			Debug.Log("ARROW WIN MAYBE");
		}
	}

	private static void ExitBlockerPlaceState(int blockType)
	{
		if (instance.myBlockerManager.SpawnBlocker(blockType) == true)
		{
            if (blockType != 3)
            {
                instance.Pause();
                EventManager.FireEvent("ObjectSelected");
            }
		}
      else
      {
			Debug.Log("NOT ENOUGH");
      }
	}

	void StartGame()
	{
		Debug.Log("START GAME");
        Resume();
        SceneManager.LoadScene("MainGameScene");
	}

	void QuitGame()
	{
		Application.Quit();
	}

	void LoadMenu()
	{
        Resume();
        SceneManager.LoadScene("MenuScene");
	}
	void LoadWin()
	{
        Resume();
		SceneManager.LoadScene("WinScene");
	}
	void LoadLose()
	{
        Resume();
        SceneManager.LoadScene("LoseScene");
	}
    void LoadTutorial()
    {
        Resume();
        SceneManager.LoadScene("TutorialScene");
    }
    void LoadTutorial2()
    {
        Resume();
        SceneManager.LoadScene("TutorialScene2");
    }

    void Pause()
	{
		Time.timeScale = 0.1f;
		Time.fixedDeltaTime = 0.02f * Time.timeScale;
	}
	void Resume()
	{
		Time.timeScale = 1.0f;
		Time.fixedDeltaTime = 0.02f * Time.timeScale;
	}

    public static void GoToEnd()
    {
        instance.Invoke("End", 4.0f);
    }

    void End()
    {
        LoadWin();
    }

	public static void ActivateUIElement(int i)
	{
		instance.elementUIImages[i].color = instance.abilityColors[i];
		instance.elementUIPulses[i].SetActive(true);
   }
   public static void DeactivateUIElement(int i)
   {
		Color32 tmpColor = new Color32(instance.abilityColors[i].r, instance.abilityColors[i].g, instance.abilityColors[i].b, 50);

		instance.elementUIImages[i].color = tmpColor;
		instance.elementUIPulses[i].SetActive(false);
	}

	public static void arrowBitFound()
	{
		instance.arrowBits++;
	}
}
