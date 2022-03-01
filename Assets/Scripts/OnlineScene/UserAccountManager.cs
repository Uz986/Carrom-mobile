using System.Collections;
using DatabaseControl;
using UnityEngine;

public class UserAccountManager : MonoBehaviour {

    public static UserAccountManager instance;

    void Awake()
    {
      
        if(instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);

    }

    //These store the username and password of the player when they have logged in
    public static string LoggedIn_playerUsername { get; protected set; }
    private static string LoggedIn_playerPassword = "";

    public static bool IsLoggedIn { get; protected set; }

    public delegate void OnDataReceivedCallback(string data);
    public void LogOut()
    {
        LoggedIn_playerUsername = "";
        LoggedIn_playerPassword = "";

        IsLoggedIn = false;

        Debug.Log("User Logged out!");
    }

    public void LogIn(string _username,string _password)
    {
        LoggedIn_playerUsername = _username;
        LoggedIn_playerPassword = _password;

        Debug.Log("User Logged in as "+_username);
        IsLoggedIn = true;

      //  GameObject.Find("Manager").GetComponent<leaderBoard>().getdataFromdatabase();
    }


    #region data

    public void SendData(string data)
    {
        if (IsLoggedIn)
        {
            StartCoroutine(SetSendData(LoggedIn_playerUsername,LoggedIn_playerPassword,data));
        }
        }
    public void GetData(OnDataReceivedCallback onDataReceived)
    {
        if (IsLoggedIn)
        {
            StartCoroutine(GetgetData(LoggedIn_playerUsername, LoggedIn_playerPassword, onDataReceived));
        }

    }

    IEnumerator GetgetData(string _username,string _password, OnDataReceivedCallback onDataReceived)
    {
        string data = "ERROR";

        IEnumerator e = DCF.GetUserData(_username, _password); // << Send request to get the player's data string. Provides the username and password
        while (e.MoveNext())
        {
            yield return e.Current;
        }
        string response = e.Current as string; // << The returned string from the request

        if (response == "Error")
        {
            //There was another error. Automatically logs player out. This error message should never appear, but is here just in case.
            Debug.Log("Error recieving data");
        }
        else
        {
            data = response;
        }
        if(onDataReceived!=null)
        onDataReceived.Invoke(data);

    }
    IEnumerator SetSendData(string _username,string _password,string data)
    {
        IEnumerator e = DCF.SetUserData(_username, _password, data); // << Send request to set the player's data string. Provides the username, password and new data string
        while (e.MoveNext())
        {
            yield return e.Current;
        }
        string response = e.Current as string; // << The returned string from the request

        if (response != "Success")
        {
            Debug.Log("Upload error");
        }
       
    }

    #endregion

}
