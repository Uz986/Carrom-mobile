using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class AddsHandler : MonoBehaviour 
{
	public string VideoAdID;
	
	RewardBasedVideoAd MyRewardVideoAD;
	void Start () 
	{
		
		MyRewardVideoAD = RewardBasedVideoAd.Instance;

		//LoadVideoAd ();
	}
	void Update () 
	{	
		

	}
	public  void LoadVideoAd ()
	{
		if(!MyRewardVideoAD.IsLoaded())
		{
			AdRequest request = new AdRequest.Builder ().Build ();
			MyRewardVideoAD.LoadAd (request, VideoAdID);
			print ("add loaded");
		}
		
	}
	public void ShowVideoAd ()
	{
		if(MyRewardVideoAD.IsLoaded())
		{	
			MyRewardVideoAD.OnAdRewarded += HandleRewardBasedVideoRewarded;
			MyRewardVideoAD.Show();
			print ("showed");
			
		}
		else 
		{
			LoadVideoAd ();
		}
	}
	public void HandleRewardBasedVideoRewarded(object sender, Reward args)
	{
		string type = args.Type;
		double amount = args.Amount;
		int coinScore;
		//Reawrd User here
		print("User rewarded with: " + amount.ToString() + " " + type);
		//Manager.life++;
		coinScore = PlayerPrefs.GetInt("Coins", 0);
		coinScore += 50;
		PlayerPrefs.SetInt("Coins", coinScore);
	}
}