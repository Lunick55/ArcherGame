using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetSpawner : MonoBehaviour {

	GameObject target;
	GameObject tentacle;
	float timer = 0;

	[SerializeField] private float waveDuration;
	[SerializeField] private int progressThroughWave = 0;
	private float oldTime = 0;

	[SerializeField] AnimationCurve difficultyCurve;

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

		if (oldTime > (Time.time % waveDuration))
		{
			progressThroughWave++;
		}

		if (timer > difficultyCurve.Evaluate(progressThroughWave)*100)
		{
			int num;
			num = Random.Range(0, 2);
			timer = 0;

			if (progressThroughWave % 10 == 0)
				SpawnEnemy(tentacle);

			//if (num == 2)
				SpawnEnemyInteresting(target);
			//if (num == 2)
				//SpawnEnemy(tentacle);
		}

		oldTime = (Time.time % waveDuration);
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
}
