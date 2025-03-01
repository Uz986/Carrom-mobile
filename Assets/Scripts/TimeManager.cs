﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

	public static TimeManager sharedInstance = null;
	private string _url = "http://localhost/Carom%20Take%202/Assets/Reward.php"; //change this to your own
	private string _timeData;
	private string _currentTime;
	private string _currentDate;


	//make sure there is only one instance of this always.
	void Awake() {
		if (sharedInstance == null) {
			sharedInstance = this;
		} else if (sharedInstance != this) {
			Destroy (gameObject);  
		}
		DontDestroyOnLoad(gameObject);
	}


	//time fether coroutine
	public IEnumerator getTime()
	{
		Debug.Log ("connecting to php");
		WWW www = new WWW (_url);
		yield return www;
		if (www.error != null) {
			Debug.Log ("Error");
		} else {
			Debug.Log ("got the php information");
		}
		_timeData = www.text;
		string[] words = _timeData.Split('/');    
		//timerTestLabel.text = www.text;
		Debug.Log ("The date is : "+words[0]);
		Debug.Log ("The time is : "+words[1]);

		//setting current time
		_currentDate = words[0];
		_currentTime = words[1];
	}

	//get the current time at startup
	void Start()
	{
		Debug.Log ("TimeManager script is Ready.");
		StartCoroutine ("getTime");
	}

	//get the current date - also converting from string to int.
	//where 12-4-2017 is 1242017
	public int getCurrentDateNow()
	{
		string[] words = _currentDate.Split('-');
		int x = int.Parse(words[0]+ words[1] + words[2]);
		return x;
	}


	//get the current Time
	public string getCurrentTimeNow()
	{
		return _currentTime;
	}


}