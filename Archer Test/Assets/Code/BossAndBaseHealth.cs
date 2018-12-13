using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAndBaseHealth : MonoBehaviour {

	[SerializeField] private int health;
	public string deathCondition;
	int damage;
	[SerializeField] private int damageThresh = 0;
	private int orgDamageThresh;

    public GameObject[] healthOrbs;
    private int orbIndex;

	// Use this for initialization
	void Start () 
	{
		orgDamageThresh = damageThresh;		

		health = transform.childCount;
        orbIndex = health - 1;


        healthOrbs = new GameObject[health];

		for (int i = 0; i < health; i++)
		{
			float randNum = (int)Random.Range(1, 3);
			randNum *= 0.3333f;

            healthOrbs[i] = transform.GetChild(i).gameObject;

			healthOrbs[i].GetComponent<Animator>().SetFloat("offset",randNum);
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
		if (!WorldManager.isTut)
		{
			if (health - dmg < 0)
			{
				if (health != 0 && orbIndex > -1)
				{
					health = 0;
					//kill kid
					Destroy(healthOrbs[orbIndex].gameObject);
					orbIndex--;
					return;
				}
				return;
			}

			health -= dmg;
			//kill kid
			healthOrbs[orbIndex].GetComponent<Animator>().SetBool("dead", true);
			Destroy(healthOrbs[orbIndex].gameObject, 0.5f);
			orbIndex--;
		}
	}

	public void DamageWallSlow(int dmg)
	{
		if (!WorldManager.isTut)
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
}
