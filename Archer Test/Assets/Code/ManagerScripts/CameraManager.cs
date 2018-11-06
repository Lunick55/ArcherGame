using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	private static CameraManager cameraManager;

	//pick one. probs time

	private Vector3 camPosOrigin;
	private Camera myCam;

	private bool isShaking = false;

	[SerializeField]
	private float shakeTime = 0;
	[SerializeField]
	private float shakeAmount = 0;
	[SerializeField]
	private float lerpSpeed = 0;

	private Vector3 dest;

	public static CameraManager instance
	{
		get
		{
			if (!cameraManager)
			{
				cameraManager = FindObjectOfType(typeof(CameraManager)) as CameraManager;

				cameraManager.Init();
			}

			return cameraManager;
		}
	}

	void Init()
	{
		instance.myCam = GetComponent<Camera>();
		instance.camPosOrigin = instance.myCam.transform.position;
	}

	public static void ShakeCamera()
	{
		if (!instance.isShaking)
		{
			instance.isShaking = true;
			instance.InvokeRepeating("StartShaking", 0, 0.05f);
			instance.Invoke("StopShaking", instance.shakeTime);
		}
	}

	private void StartShaking()
	{
		if (instance.myCam.transform.position == instance.camPosOrigin)
		{
			//if at origin, go somewhere else
			Vector3 moveAmt = Random.insideUnitSphere * instance.shakeAmount;
			dest = instance.myCam.transform.position += moveAmt;
		}
		else if (instance.myCam.transform.position == dest)
		{
			//if we are at dest, work back to origin
			dest = instance.camPosOrigin;
		}

		instance.myCam.transform.position = Vector3.Lerp(instance.myCam.transform.position, dest, lerpSpeed * Time.deltaTime);

	}

	private void StopShaking()
	{
		instance.CancelInvoke("StartShaking");
		instance.isShaking = false;
		instance.myCam.transform.position = instance.camPosOrigin;
	}
}
