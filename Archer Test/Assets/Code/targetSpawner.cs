using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetSpawner : MonoBehaviour {

	GameObject target;
	GameObject tentacle;
	float timer = 0;
	public float timeToSpawn;
	float timeToMakeHarder;//decrease health by x every interval
	public float difficultyInterval;
	public float maxSpawnRate;
	public float amountDecreased;

	// Use this for initialization
	void Start () 
	{
		target = Resources.Load("target") as GameObject;
		tentacle = Resources.Load("tentacle") as GameObject;
	}

	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;
		timeToMakeHarder += Time.deltaTime;

		if (timeToSpawn >= maxSpawnRate && timeToMakeHarder > difficultyInterval)
		{
			timeToSpawn -= amountDecreased;
			timeToMakeHarder = 0;
		}
		if (timer > timeToSpawn)
		{
			int num;
			num = Random.Range(0, 2);

			if (num == 0)
				SpawnEnemy(target);
			if (num == 1)
				SpawnEnemy(tentacle);
		}
	}

	void SpawnEnemy(GameObject enemy)
	{
		GameObject newEnemy = Instantiate(enemy) as GameObject;
		newEnemy.transform.SetPositionAndRotation(new Vector3(transform.position.x, Random.Range(-3, 3), 0), Quaternion.identity);
		timer = 0;
	}

	void IncreaseSpawnRate()
	{

	}
}
