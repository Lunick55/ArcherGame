using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetSpawner : MonoBehaviour {

	GameObject target;
	float timer = 0;
	public float timeToSpawn;

	// Use this for initialization
	void Start () 
	{
		target = Resources.Load("target") as GameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.Space))
		{
			GameObject newTarget = Instantiate(target) as GameObject;
			target.transform.SetPositionAndRotation(new Vector3(transform.position.x, Random.Range(-3, 5), 0), Quaternion.identity);
		}
		if (timer > timeToSpawn)
		{
			GameObject newTarget = Instantiate(target) as GameObject;
			target.transform.SetPositionAndRotation(new Vector3(transform.position.x, Random.Range(-3, 5), 0), Quaternion.identity);
			timer = 0;
		}
	}
}
