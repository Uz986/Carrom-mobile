using System.Collections;
using UnityEngine;

public class PlayerScore : MonoBehaviour {

    int wonnow = 0;
    int lossnow = 0;
    StrickerOnline player;
	void Start () {
        player = GetComponent<StrickerOnline>();
       
    }
	
  

    void OnDestroy()
    {
        if(player!=null)
        SyncNow();
    }

    void SyncNow()
    {
        if (!player.GameOver)
            return;
      

        UserAccountManager.instance.GetData(onDataReceived);
    }
    void onDataReceived(string data)
    {
        if (player.IWon)
        {
            wonnow = 1;

            int temp = PlayerPrefs.GetInt("Coins", 0);
            Debug.Log(temp+" coins get");
            PlayerPrefs.SetInt("Coins", temp + 50);
        }
        else
        {
            lossnow = 1;
        }

        int wins = DataTranslator.DatatoKills(data);
        int loss = DataTranslator.DatatoDeaths(data);

        int newKills = wonnow + wins;
        int newDeaths = lossnow + loss;

        string newData = DataTranslator.ValuestoData(newKills,newDeaths);

        Debug.Log("Syncing "+newData);


        UserAccountManager.instance.SendData(newData);
    }
}
