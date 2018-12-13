using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lingerSound : MonoBehaviour {

	AudioSource audioSource;
	AudioClip clipToPlay;

	// Use this for initialization
	void Awake () 
	{
		audioSource = GetComponent<AudioSource>();
	}

	void die()
	{
		audioSource.clip = clipToPlay;
		audioSource.Play();
		Destroy(gameObject, audioSource.clip.length);
	}

	public void setClip(AudioClip newClip)
	{
		clipToPlay = newClip;
		die();
	}
}
