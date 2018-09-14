using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineRenderScript : MonoBehaviour {

	LineRenderer lr;
	Renderer rend;

	public float velocity;
	public float angle;
	public int resolution;
	public int extension = 1;

	float g;
	float radianAngle;

	// Use this for initialization
	void Start () 
	{
		lr = GetComponent<LineRenderer>();
		g = Mathf.Abs(Physics2D.gravity.y);
		rend = GetComponent<Renderer>();

		RenderArc();	
	}
	
	// Update is called once per frame
	void Update () 
	{
		RenderArc();	
	}

	void RenderArc()
	{
		lr.positionCount = (resolution + extension);
		lr.SetPositions(CalculateArcArray());
	}

	Vector3[] CalculateArcArray()
	{
		Vector3[] arcArrray = new Vector3[resolution + extension];

		radianAngle = Mathf.Deg2Rad * angle;

		float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / g;

		for (int i = 0; i <= (resolution + (extension - 1)); i++)
		{
			float t = (float)i / (float)resolution;
			arcArrray[i] = CalculateArcPoint(t, maxDistance);
		}

		return arcArrray;
	}

	Vector3 CalculateArcPoint(float t, float maxDistance)
	{
		float x = (t * maxDistance);
		float y = x * Mathf.Tan(radianAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));

		return new Vector3(x + gameObject.transform.position.x, y + gameObject.transform.position.y);
	}
}
