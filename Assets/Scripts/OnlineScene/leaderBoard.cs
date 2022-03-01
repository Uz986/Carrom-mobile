using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PubNubAPI;
using UnityEngine.UI;
//using SimpleJSON;

public class MyClass
{
    public string username;
    public string score;
    public string test;
}

public class leaderBoard : MonoBehaviour
{
    public static PubNub pubnub;
    public Text Line1;
    public Text Line2;
    public Text Line3;
    public Text Line4;
    public Text Line5;
    public Text Score1;
    public Text Score2;
    public Text Score3;
    public Text Score4;
    public Text Score5;

   
    //public Object[] tiles = {}
    // Use this for initialization
    void Start()
    {


        // Use this for initialization
        PNConfiguration pnConfiguration = new PNConfiguration();
        pnConfiguration.PublishKey = "pub-c-6c6afb92-fc0e-4af5-a180-52adf89c135e";
        pnConfiguration.SubscribeKey = "sub-c-0af0a952-65a9-11e9-acd4-021bd504a859";

        pnConfiguration.LogVerbosity = PNLogVerbosity.BODY;
        if(PlayerPrefs.HasKey(UserAccountManager.LoggedIn_playerUsername))
        {
            pnConfiguration.UUID = PlayerPrefs.GetFloat(UserAccountManager.LoggedIn_playerUsername).ToString();
        }
        else
        {
            float temp= Random.Range(0f, 999999f);
            PlayerPrefs.SetFloat(UserAccountManager.LoggedIn_playerUsername, temp);
            pnConfiguration.UUID = temp.ToString();
        }
       



        pubnub = new PubNub(pnConfiguration);
        Debug.Log(pnConfiguration.UUID);


        MyClass myFireObject = new MyClass();
        myFireObject.test = "new user";
        string fireobject = JsonUtility.ToJson(myFireObject);
        pubnub.Fire()
            .Channel("MyChannelH")
            .Message(fireobject)
            .Async((result, status) => {
                if (status.Error)
                {
                    Debug.Log(status.Error);
                    Debug.Log(status.ErrorData.Info);
                }
                else
                {
                    Debug.Log(string.Format("Fire Timetoken: {0}", result.Timetoken));
                }
            });

        pubnub.SusbcribeCallback += (sender, e) => {
            SusbcribeEventEventArgs mea = e as SusbcribeEventEventArgs;
            if (mea.Status != null)
            {
            }
            if (mea.MessageResult != null)
            {
                Dictionary<string, object> msg = mea.MessageResult.Payload as Dictionary<string, object>;

                string[] strArr = msg["username"] as string[];
                string[] strScores = msg["score"] as string[];

                int usernamevar = 1;
                foreach (string username in strArr)
                {
                    string usernameobject = "Line" + usernamevar;
                    GameObject.Find(usernameobject).GetComponent<Text>().text = usernamevar.ToString() + ". " + username.ToString();
                    usernamevar++;
                    Debug.Log(username);
                }

                int scorevar = 1;
                foreach (string score in strScores)
                {
                    string scoreobject = "Score" + scorevar;
                    GameObject.Find(scoreobject).GetComponent<Text>().text = "Score: " + score.ToString();
                    scorevar++;
                    Debug.Log(score);
                }
            }
            if (mea.PresenceEventResult != null)
            {
                Debug.Log("In Example, SusbcribeCallback in presence" + mea.PresenceEventResult.Channel + mea.PresenceEventResult.Occupancy + mea.PresenceEventResult.Event);
            }
        };
        pubnub.Subscribe()
            .Channels(new List<string>() {
                "MyChannelH2"
            })
            .WithPresence()
            .Execute();
    }

    public void getdataFromdatabase()
    {
        UserAccountManager.instance.GetData(onDataReceived);
    }
    void onDataReceived(string data)
    {
        float wins = DataTranslator.DatatoKills(data);
        //float loss = DataTranslator.DatatoDeaths(data);
        //float ratio;
        //if(loss==0)
        //{
        //    ratio = wins;
        //}
        //else
        //{
        //    ratio = wins / loss;
        //}

        //Debug.Log(ratio+" ratio"+ wins + " wins" +loss +" loss");
        string name = UserAccountManager.LoggedIn_playerUsername;
        SendleaderboardData(name, wins.ToString());
    }

     void SendleaderboardData(string username,string score)
    {
        var usernametext = username;// this would be set somewhere else in the code
        var scoretext = score;
        MyClass myObject = new MyClass();
        myObject.username = username;
        myObject.score = score;
        string json = JsonUtility.ToJson(myObject);

        pubnub.Publish()
            .Channel("MyChannelH")
            .Message(json)
            .Async((result, status) => {
                if (!status.Error)
                {
                    Debug.Log(string.Format("Publish Timetoken: {0}", result.Timetoken));
                }
                else
                {
                    Debug.Log(status.Error);
                    Debug.Log(status.ErrorData.Info);
                }
            });
        //Output this to console when the Button is clicked
        
    }

}