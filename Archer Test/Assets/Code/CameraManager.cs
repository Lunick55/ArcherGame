﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	private static CameraManager cameraManager;

	//pick one. probs time

	private Vector3 camPosOrigin;
	private Camera myCam;

	[SerializeField]
	private float shakeTime = 0;
	[SerializeField]
	private float shakeAmount = 0;	

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
		Debug.Log("SHAKE");
		instance.InvokeRepeating("StartShaking", 0, 0.1f);
		instance.Invoke("StopShaking", instance.shakeTime);		
	}

	private void StartShaking()
	{
		if (instance.myCam.transform.position != instance.camPosOrigin)
		{
			instance.myCam.transform.position = instance.camPosOrigin;
		}
		else
		{
			Vector3 moveAmt = Random.insideUnitSphere * instance.shakeAmount;
			instance.myCam.transform.position += moveAmt;
		}
	}

	private void StopShaking()
	{
		instance.CancelInvoke("StartShaking");
		instance.myCam.transform.position = instance.camPosOrigin;
	}
}
