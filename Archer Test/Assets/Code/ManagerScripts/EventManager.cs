using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {

	Dictionary<string, UnityEvent> eventLog;

	private static EventManager eventManager;

	public static EventManager instance
	{
		get
		{
			if (!eventManager)
			{
				eventManager = FindObjectOfType (typeof (EventManager)) as EventManager;

				eventManager.Init();
			}

			return eventManager;
		}
	}

	void Init()
	{
		if (eventLog == null)
		{
			eventLog = new Dictionary<string, UnityEvent>();
		}
	}

	public static void AddListener(string eventName, UnityAction listener)
	{
		UnityEvent thisEvent = null;
		if (instance.eventLog.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.AddListener(listener);
		}
		else
		{
			thisEvent = new UnityEvent();
			thisEvent.AddListener(listener);
			instance.eventLog.Add(eventName, thisEvent);
		}
	}

	public static void StopListening(string eventName, UnityAction listener)
	{
		if (eventManager == null) return;

        UnityEvent thisEvent = null;

        if (instance.eventLog.TryGetValue (eventName, out thisEvent))
        {
            thisEvent.RemoveListener (listener);
        }
	}

	public static void FireEvent(string eventName)
	{
		UnityEvent thisEvent = null;

		if (instance.eventLog.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.Invoke();
		}
	}

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
