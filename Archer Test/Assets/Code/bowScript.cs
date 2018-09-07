using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowScript : MonoBehaviour {

    Vector3 mousePos;
    private float angle;

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition);

        Rotate();
	}

    void Rotate()
    {
        Vector3 distance = (mousePos - gameObject.transform.position);

        angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
