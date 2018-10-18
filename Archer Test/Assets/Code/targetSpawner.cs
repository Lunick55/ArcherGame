using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetSpawner : MonoBehaviour {

	GameObject target;
	GameObject tentacle;
	float timer = 0;
	public float timeToSpawn;

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

		if (Input.GetKeyDown(KeyCode.Space))
		{
			GameObject newEnemy = Instantiate(tentacle) as GameObject;
			newEnemy.transform.SetPositionAndRotation(new Vector3(transform.position.x, Random.Range(-3, 5), 0), Quaternion.identity);
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
		newEnemy.transform.SetPositionAndRotation(new Vector3(transform.position.x, Random.Range(-3, 5), 0), Quaternion.identity);
		timer = 0;
	}
}
