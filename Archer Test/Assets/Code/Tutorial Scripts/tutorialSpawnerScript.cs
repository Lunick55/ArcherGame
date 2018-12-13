using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialSpawnerScript : MonoBehaviour
{
	//
	int tutEnemiesDead = 0;
	int tutAbilities = 0;
	//

	GameObject target;
	GameObject tentacle;

	[SerializeField] private float waveDuration;

	// Use this for initialization
	void Start()
	{
		target = Resources.Load("target") as GameObject;
		tentacle = Resources.Load("tentacle") as GameObject;

		EventManager.AddListener("MedFill", AnotherBitesDust);
		EventManager.AddListener("SmallFill", AnotherBitesDust);

		EventManager.AddListener("ObjectPlaced", AnotherAbility);
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			SpawnEnemy(target);
		}
		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
		{
			SpawnEnemy(tentacle);
		}

		if (tutEnemiesDead >= 5 && tutEnemiesDead < 7)
		{
			EventManager.FireEvent("NextBox");
			tutEnemiesDead = 20;
		}

		if (tutAbilities >= 2 && tutAbilities < 7)
		{
			EventManager.FireEvent("NextBox");
			tutAbilities = 20;
		}
	}

	void SpawnEnemy(GameObject enemy)
	{
		GameObject newEnemy = Instantiate(enemy) as GameObject;
		newEnemy.transform.SetPositionAndRotation(new Vector3(transform.position.x, Random.Range(-3, 3), 0), Quaternion.identity);
	}

	void SpawnEnemyInteresting(GameObject enemy)
	{
		float size;
		float speed;
		int colorG;
		size = Random.Range(1.0f, 3.0f);
		speed = Random.Range(-5.0f, -8.0f);
		colorG = Random.Range(0, 255);

		GameObject newEnemy = Instantiate(enemy) as GameObject;
		Transform enemyTrans = newEnemy.transform;

		enemyTrans.SetPositionAndRotation(new Vector3(transform.position.x, Random.Range(-3, 3), 0), Quaternion.identity);
		enemyTrans.localScale = new Vector3(size, size, 0);
		enemyTrans.GetComponent<enemyScript>().ChangeSpeed(speed);
		enemyTrans.GetComponent<SpriteRenderer>().color = new Color32(255, (byte)colorG, 255, 255);
	}

	void AnotherBitesDust()
	{
		tutEnemiesDead++;
	}

	void AnotherAbility()
	{
		tutAbilities++;
	}
}
