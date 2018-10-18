using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAndBaseHealth : MonoBehaviour {

	public int health;
	public string deathCondition;
	int damage;
	private int maxHealth;
	SpriteRenderer rend;

	// Use this for initialization
	void Start () 
	{
		maxHealth = health;
		rend = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (health <= 0)
		{
			EventManager.FireEvent(deathCondition);
		}
		damage = (255 * (health * 100 / maxHealth) / 100);

		rend.color = new Color32(255, (byte)damage, (byte)damage, 255);
	}
}
