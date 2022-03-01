using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

    int strickerindex= 1;

    [SerializeField]
    GameObject stricker;

    [SerializeField]
    Text Pricetxt;

    [SerializeField]
    GameObject locked;

    string mat = "Mat";

    [SerializeField]
    GameObject shopanel;

    [SerializeField]
    GameObject mainmenu;

    [SerializeField]
    Text Coinsdisplay;

    int price = 50;
    void Start()
    {
        if (!PlayerPrefs.HasKey("Mat1"))
        {
            PlayerPrefs.SetInt("Mat1", 0);
        }
        if (!PlayerPrefs.HasKey("Mat2"))
        {
            PlayerPrefs.SetInt("Mat2", 0);
        }
        if (!PlayerPrefs.HasKey("Mat3"))
        {
            PlayerPrefs.SetInt("Mat3", 0);
        }
        if (!PlayerPrefs.HasKey("Mat4"))
        {
            PlayerPrefs.SetInt("Mat4", 0);
        }

    }

   public void ChangeStricker(int i)
    {
       
        strickerindex = strickerindex + i;
        if(strickerindex>4)
        {
            strickerindex = strickerindex - 1;
            return;
        }
        else if(strickerindex <1)
        {
            strickerindex = strickerindex + 1;
            return;
        }

        string temp = mat + strickerindex;
       if(PlayerPrefs.GetInt(temp)==1)
        {
            locked.SetActive(false);
        }
       else
        {
            locked.SetActive(true);
        }

        Pricetxt.text = (price * strickerindex).ToString();

        stricker.GetComponent<Renderer>().sharedMaterial = Resources.Load(strickerindex.ToString(), typeof(Material)) as Material;
    }

    public void Buy()
    {
        int coin = PlayerPrefs.GetInt("Coins");
        if (coin >= (price * strickerindex))
        {
            coin = coin - (price * strickerindex);
            PlayerPrefs.SetInt("Coins",coin);

            Coinsdisplay.text = coin.ToString();

            string temp = mat + strickerindex;
            PlayerPrefs.SetInt(temp, 1);
            locked.SetActive(false);
        }
       
    }

    public void shoppanel()
    {
        mainmenu.SetActive(false);
        shopanel.SetActive(true);

        string temp = mat + strickerindex;
        if (PlayerPrefs.GetInt(temp,0) == 1)
        {
            locked.SetActive(false);
        }
        else
        {
            locked.SetActive(true);
        }
    }

    public void selectgoti()
    {
        string temp = mat + strickerindex;
       if (PlayerPrefs.GetInt(temp, 0)!=1)
        {
            return;
        }

        PlayerPrefs.SetInt("CurrentStricker",strickerindex);
        Debug.Log(strickerindex+" stricker selected");
    }

    public void backtomain()
    {
        mainmenu.SetActive(true);
        shopanel.SetActive(false);
    }
}
