using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour 
{
	[SerializeField] GameObject notEnoughObject;
	[SerializeField] Animator transitionAnim;

	SpriteRenderer notEnoughColor;
	float notEnoughTimer = 0;

	public static bool tutAbilityActive = false;
	public static bool tutEndSoon = false;
	public static bool isTut = false;

	public GameObject BlockerManager;
	BlockerManagerScript myBlockerManager;

	[SerializeField] int bitsToWin = 0;
	private int arrowBits = 0; //number to win game

	private static WorldManager worldManager;

   [SerializeField] Image[] elementUIImages;
	[SerializeField] GameObject[] elementUIPulses;
	[SerializeField] Sprite[] finalArrowProgress;
	[SerializeField] GameObject finalArrow;

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
		if (notEnoughObject != null)
		{
			notEnoughColor = notEnoughObject.GetComponent<SpriteRenderer>();
		}

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
      //EventManager.AddListener("LoadTutorial2", LoadTutorial2);

      EventManager.AddListener("ObjectPlaced", Resume);
	}

	// Update is called once per frame
	void Update () 
	{
		if (SceneManager.GetActiveScene().name == "TutorialScene")
		{
			isTut = true;
		}
		else
		{
			isTut = false;
		}

		if (BlockerManager != null)
		{
			if (SceneManager.GetActiveScene().name == "TutorialScene")
			{
				if (tutAbilityActive == true)
				{
					if (Time.timeScale != 0.1f)
					{
						if (Input.GetKeyDown(KeyCode.Alpha2))
						{
							ExitBlockerPlaceState(2);//barrier
						}
						if (Input.GetKeyDown(KeyCode.Alpha1))
						{
							ExitBlockerPlaceState(1);//sentry
						}
					}
				}
			}
			else if (Time.timeScale != 0.1f)
			{
				if (Input.GetKeyDown(KeyCode.Alpha2))
				{
					ExitBlockerPlaceState(2);//barrier
				}
				if (Input.GetKeyDown(KeyCode.Alpha1))
				{
					ExitBlockerPlaceState(1);//sentry
				}
			}
         if (Input.GetKeyDown(KeyCode.Escape))
         {
				LoadMenu();
         }
		}

		if (instance.arrowBits >= instance.bitsToWin && SceneManager.GetActiveScene().name == "MainGameScene")
		{
			EventManager.FireEvent("GAMEOVER");
			GoToEnd();
		}
		if (instance.arrowBits >= instance.bitsToWin && SceneManager.GetActiveScene().name == "TutorialScene")
		{
			instance.arrowBits = -10000;
			EventManager.FireEvent("NextBox");
		}

		if (notEnoughTimer > 0)
		{
			notEnoughTimer -= Time.deltaTime;
			notEnoughColor.color = new Color(Mathf.Abs(Mathf.Sin(Time.time*5)), 0, 0, 1);
		}
		else
		{
			notEnoughObject.SetActive(false);
		}
	}

	public static bool FireAutoArrow()
	{
		if (instance.myBlockerManager.SpawnBlocker(3) == true)
		{
			return true;
		}
		return false;
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
			instance.notEnoughTimer = 1;
			instance.notEnoughObject.SetActive(true);
		}
	}

	void StartGame()
	{
		Debug.Log("START GAME");
        Resume();
		//SceneManager.LoadScene("MainGameScene");
		StartCoroutine(LoadScene("MainGameScene"));
	}

	void QuitGame()
	{
		Application.Quit();
	}

	void LoadMenu()
	{
        Resume();
		StartCoroutine(LoadScene("MenuScene"));
	}
	void LoadWin()
	{
      Resume();
		StartCoroutine(LoadScene("WinScene"));
	}
	void LoadLose()
	{
        Resume();
		StartCoroutine(LoadScene("LoseScene"));
	}
    void LoadTutorial()
    {
        Resume();
		StartCoroutine(LoadScene("TutorialScene"));
    }
    //void LoadTutorial2()
    //{
    //    Resume();
    //    SceneManager.LoadScene("TutorialScene2");
    //}

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
		Debug.Log("ACTIVATE");
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
		if (SceneManager.GetActiveScene().name == "TutorialScene" && tutEndSoon)
		{
			EventManager.FireEvent("DeadMansHand");
			instance.arrowBits++;
			instance.finalArrow.GetComponent<SpriteRenderer>().sprite = instance.finalArrowProgress[0];
		}
		else 
		{
			EventManager.FireEvent("DeadMansHand");
			instance.arrowBits++;
			instance.finalArrow.GetComponent<SpriteRenderer>().sprite = instance.finalArrowProgress[instance.arrowBits - 1];
		}
	}

	IEnumerator LoadScene(string sceneName){
		transitionAnim.SetTrigger("end");
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene(sceneName);
	}
}
