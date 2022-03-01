using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OnlineScore   : MonoBehaviour {



    public static GameObject redgoti;


    StrickerOnline strickerturn;



    private void OnCollisionEnter(Collision collision)
    {

        StrickerOnline[] strickeronline = GameManager.GetAllPlayers();
        foreach (StrickerOnline st in strickeronline)
        {
           
            if (st.MyTurn)
            {
                
                if (st.isLocalPlayer)
                {
                  
                    if (collision.gameObject.tag == "WGoti")
                    {
                       
                        Debug.Log(collision.gameObject.name);
                  
                       
                        st.CmdScore(collision.gameObject, 10);

                    }
                    else if (collision.gameObject.tag == "BGoti")
                    {
                       
                        Debug.Log(collision.gameObject.name);
                   
                       
                        st.CmdScore(collision.gameObject, 5);
                    }
                    else if (collision.gameObject.tag == "RGoti")
                    {
                       
                        Debug.Log(collision.gameObject.name);
                  
                       
                        st.CmdScore(collision.gameObject, 50);


                    }

                }
            }
        }

       
    }

   
}

