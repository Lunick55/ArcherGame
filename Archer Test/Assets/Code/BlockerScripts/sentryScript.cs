using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class sentryScript : MonoBehaviour {
	
	[SerializeField] AudioClip sentryPlacedSound;
	[SerializeField] AudioClip sentryDamagedSound;
	[SerializeField] AudioClip sentryDeadSound;

	[SerializeField] AudioSource sentrySound;

	Vector2 mousePos;
	bool activated = false;
	bool canPlace = true;
	[SerializeField] int SentryStrength;
	GameObject[] childSentry;
	int currSentry = 2;

	// Use this for initialization
	void Start () 
	{
		childSentry = new GameObject[3];

		childSentry[0] = transform.GetChild(0).gameObject;
		childSentry[1] = transform.GetChild(1).gameObject;
		childSentry[2] = transform.GetChild(2).gameObject;

		childSentry[0].GetComponent<Animator>().SetFloat("Offset", 0);
		childSentry[1].GetComponent<Animator>().SetFloat("Offset", 0.33f);
		childSentry[2].GetComponent<Animator>().SetFloat("Offset", 0.66f);

		sentrySound = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () 
	{
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		GetInput();

		if (activated == false)
		{
			//follow mouse
			transform.position = mousePos;
		}
		else
		{
			SentryMode();
		}

		if (SentryStrength <= 0)
		{
			Destroy(gameObject);
		}
	}

	void GetInput()
	{
		if (EventSystem.current.IsPointerOverGameObject() == true)
		{
			return;
		}

		if (Input.GetMouseButtonUp(0) == true && activated == false && canPlace)
		{
			activated = true;
			sentrySound.clip = sentryPlacedSound;
			sentrySound.Play();
			EventManager.FireEvent("ObjectPlaced");
		}

		if (Input.GetMouseButtonUp(1) && !activated)
		{
			SentryStrength = 0;
			EventManager.FireEvent("ObjectPlaced");
		}
	}

	void SentryMode()
	{
		this.tag = "Sentry";
	}

	private void OnTriggerStay2D(Collider2D col)
	{
		canPlace = false;
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("IM IN");
		canPlace = false;
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		Debug.Log("IM OUT");
		canPlace = true;
	}

	public void DamageDone()
	{
		Debug.Log("SENTRY DAMAGED");
		SentryStrength--;
		Destroy(childSentry[currSentry--]);
	}
}
