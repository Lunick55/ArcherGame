using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineRenderScript : MonoBehaviour {

	LineRenderer lr;

	public float velocity;
	public float angle;
	public int resolution;

	float g;
	float radianAngle;

	// Use this for initialization
	void Start () 
	{
		lr = GetComponent<LineRenderer>();
		g = Mathf.Abs(Physics2D.gravity.y);

		RenderArc();	
	}
	
	// Update is called once per frame
	void Update () 
	{
		RenderArc();	
	}

	void RenderArc()
	{
		lr.positionCount = (resolution);
		lr.SetPositions(CalculateArcArray());

		float scaleX = Mathf.Cos(Time.time) * 0.5F + 1;
		float scaleY = Mathf.Sin(Time.time) * 0.5F + 1;
		lr.material.SetTextureScale("_MainTex", new Vector2(2.0f, 1.0f));
	}

	Vector3[] CalculateArcArray()
	{
		//convert angle to radians
		radianAngle = Mathf.Deg2Rad * angle;

		//get the arcs total distance
		float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / g;

		//create array of all the individual points
		Vector3[] arcArrray = new Vector3[resolution];

		//for each point place it on the arc
		for (int i = 0; i <= resolution - 1; i++)
		{
			float t = (float)i / (float)resolution;
			arcArrray[i] = CalculateArcPoint(t, maxDistance);

		}

		//lr.material.SetTextureScale("This IS Good", new Vector2(1, 0));

		return arcArrray;
	}

	Vector3 CalculateArcPoint(float t, float maxDistance)
	{
		float x = (t * maxDistance);
		float y = x * Mathf.Tan(radianAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));

		return new Vector3(x + gameObject.transform.position.x, y + gameObject.transform.position.y);
	}
}
