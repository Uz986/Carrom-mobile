using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Chest : MonoBehaviour {

	//public GameObject extraLifeWindow, GameOverWindow;//for self purpose
	public  Text chestTimer;//for self purpose

	public  Button chestButton;
	float msToWait = 5000;
	 ulong lastChestOpen;
	//int holder;

	// Use this for initialization
	void Start () 
	{
		//holder = 0;

		lastChestOpen = ulong.Parse (PlayerPrefs.GetString ("LastChestOpen"));

		if(!IsChestReady())
		{
			chestButton.interactable = false;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!chestButton.IsInteractable())
		{
			if(IsChestReady())
			{
				chestButton.interactable = true;

				return;
			}

			//set the timer
			ulong diff = ((ulong)DateTime.Now.Ticks - lastChestOpen);
			ulong m = diff / TimeSpan.TicksPerMillisecond;
			float secondsLeft = (float)(msToWait - m) / 1000.0f;
			string r = "";
			//hours
			//r+=((int )secondsLeft/3600).ToString()+ "h ";

			r+=((int)secondsLeft / 60) .ToString("00")+ "m ";
			r+=((int)secondsLeft % 60) .ToString("00")+ "s ";
			chestTimer.text = r;

		}

	}


	public void ChestClick () 
	{
		lastChestOpen = (ulong)System.DateTime.Now.Ticks;
		PlayerPrefs.SetString ("LastChestOpen", lastChestOpen.ToString());
		chestButton.interactable = false;


		//Perform action. reward

		Invoke ("startTimer", 5f);

	}



	private bool IsChestReady () 
	{
		ulong diff = ((ulong)DateTime.Now.Ticks - lastChestOpen);
		ulong m = diff / TimeSpan.TicksPerMillisecond;
		float secondsLeft = (float)(msToWait - m) / 1000.0f;
		print ("Minute avlue is " + m);
		print ("second avlue is " + secondsLeft);
		if (secondsLeft <0)
		{
			 
			chestTimer.text = "00:05";// its time for 5 sec, you need to make this 24 hours for daily reward by changing this time and upper "mstowait" variable also
			//extraLifeWindow.gameObject.SetActive (false);
			//GameOverWindow.gameObject.SetActive (true);
			return true;
		}



		return false;


	}
	void startTimer()
	{
        //reward here to user
        //AddsHandler.addsNumber=1;

        int temp = PlayerPrefs.GetInt("Coins",0);
        Debug.Log(temp + " coins get");
        PlayerPrefs.SetInt("Coins", temp+100);
       

    }
}
