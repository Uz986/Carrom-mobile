
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class IMP : MonoBehaviour {

    [SerializeField]
    GameObject mainmenu;

    [SerializeField]
    GameObject modeoptions;

    [SerializeField]
    GameObject multimenu;

    [SerializeField]
    GameObject leaderboard;

    [SerializeField]
    GameObject loginpanel;


    [SerializeField]
    GameObject Join;

    int Coin;

    [SerializeField]
    Text CoinText;

    [SerializeField]
    Text Username;

    void Start()
    {
        if(UserAccountManager.IsLoggedIn)
        {
            mainmenu.SetActive(true);
            loginpanel.SetActive(false);
            Username.text = UserAccountManager.LoggedIn_playerUsername;
        }
         Coin = PlayerPrefs.GetInt("Coins", 0);
        DisplayCoin();
    }

    void Update()
    {
        int temp = PlayerPrefs.GetInt("Coins");

        if (Coin< temp && temp != 0)
        {
            Coin = PlayerPrefs.GetInt("Coins");
            DisplayCoin();
        }
    }
    void DisplayCoin()
    {
        CoinText.text = Coin.ToString();
    }

    public void Practice()
    {
       
        SceneManager.LoadScene(2);
    }

    public void Main_Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void SettingsMenu()
    {
       
        SceneManager.LoadScene(1);
    }
   
    public void Quitgame()
    {
        Application.Quit();
    }
    public void StartGameSinglePlayer()
    {
       
            SceneManager.LoadScene(3);
      

    } 

    public void singlplaymenu()
    {
        mainmenu.SetActive(false);
        modeoptions.SetActive(true);
    }

    public void Multiplaymenu()
    {
        mainmenu.SetActive(false);
        multimenu.SetActive(true);
    }

   
    public void join()
    {
        multimenu.SetActive(false);
        Join.SetActive(true);
    }

    public void backtohost_join()
    {
        Join.SetActive(false);
        multimenu.SetActive(true);
    }

    public void backtomain()
    {
        multimenu.SetActive(false);
        mainmenu.SetActive(true);
    }
    public void backtomainfromsingle()
    {
        modeoptions.SetActive(false);
        mainmenu.SetActive(true);
    }

    public void leaderboardactive()
    {
        mainmenu.SetActive(false);
        leaderboard.SetActive(true);
    }

    public void leaderboardbacktomain()
    {
        mainmenu.SetActive(true);
        leaderboard.SetActive(false);
    }
}
