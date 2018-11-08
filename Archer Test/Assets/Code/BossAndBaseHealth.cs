using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAndBaseHealth : MonoBehaviour {

	[SerializeField] private int health;
	public string deathCondition;
	int damage;
	[SerializeField] private int damageThresh = 0;
	private int orgDamageThresh;

	// Use this for initialization
	void Start () 
	{
		orgDamageThresh = damageThresh;		

		health = transform.childCount;
		Debug.Log(health);
		for (int i = 0; i < health-1; i++)
		{
			float randNum = (int)Random.Range(1, 3);
			randNum *= 0.3333f;

			transform.GetChild(i).GetComponent<Animator>().SetFloat("offset",randNum);
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (health <= 0)
		{
			EventManager.FireEvent(deathCondition);
		}
	}

	public void DamageWall(int dmg)
	{
		if (health-dmg < 0)
		{
			if (health != 0)
			{
				health = 0;
				//kill kid
				Destroy(transform.GetChild(0).gameObject);
				return;
			}
			return;
		}

		health -= dmg;
		//kill kid
		Destroy(transform.GetChild(0).gameObject);
	}

	public void DamageWallSlow(int dmg)
	{ 
		if (damageThresh < 0)
		{
			DamageWall(1);
			damageThresh = orgDamageThresh;
		}
		else 
		{
			damageThresh--;
		}
	}
}
