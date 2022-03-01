using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score_2p   : MonoBehaviour {

    //  public GameObject Hole;
    [SerializeField]
    public static int PlayerScore=0;
    public static int AIScore=0;
    public Text Playerscoretext;
    public Text AIScoretext;


    public static GameObject redgoti;


    StrickerTurn strickerturn;

    void Start()
    {
        strickerturn = StrickerTurn.instance;
      
       
    }

    // Update is called once per frame
    void Update () {
        Playerscoretext.text =  PlayerScore.ToString();
        AIScoretext.text = AIScore.ToString();
    }
    private void OnCollisionEnter(Collision collision)
    {
            if(strickerturn.Isplayerturn)
        {
            if (collision.gameObject.tag == "WGoti")
            {


                if (strickerturn.queenScore)
                {
                    Destroy(redgoti);
                    PlayerScore = PlayerScore + 50;
                    strickerturn.queenScore = false;
                }

                else if(strickerturn.LastGoti!=null) //if this is last goti and queen is not potted
                {
                    strickerturn.LastGoti.GetComponent<LastGoti>().Spawn();
                    return;
                }

                PlayerScore = PlayerScore + 10;
                strickerturn.OneMoreTurn = true;
                Destroy(collision.gameObject, 0.01f);


            }
            else if (collision.gameObject.tag == "BGoti")
            {
                

                if(strickerturn.queenScore)
                {
                   Destroy(redgoti);
                    PlayerScore = PlayerScore + 50;
                    strickerturn.queenScore = false;
                }

                else if (strickerturn.LastGoti != null) //if this is last goti and queen is not potted
                {
                    strickerturn.LastGoti.GetComponent<LastGoti>().Spawn();
                    return;
                }


                PlayerScore = PlayerScore + 5;
                strickerturn.OneMoreTurn = true;
                Destroy(collision.gameObject, 0.01f);

            }
            else if (collision.gameObject.tag == "RGoti")
            {
               // PlayerScore = PlayerScore + 50;
                strickerturn.OneMoreTurn = true;
                strickerturn.queenScore = true;
            
                collision.gameObject.SetActive(false);
                redgoti = collision.gameObject;

                //Destroy(collision.gameObject, 0.01f);
            }


        }
            else
        {

            if (collision.gameObject.tag == "WGoti")
            {
                

                if (strickerturn.queenScore)
                {
                    Destroy(redgoti);
                    AIScore = AIScore + 50;
                    strickerturn.queenScore = false;
                }

                else if (strickerturn.LastGoti != null) //if this is last goti and queen is not potted
                {
                    strickerturn.LastGoti.GetComponent<LastGoti>().Spawn();
                    return;
                }

                AIScore = AIScore + 10;
                strickerturn.OneMoreTurn = true;
                Destroy(collision.gameObject);

            }
            else if (collision.gameObject.tag == "BGoti")
            {
                
                if (strickerturn.queenScore)
                {
                    Destroy(redgoti);
                    AIScore = AIScore + 50;
                    strickerturn.queenScore = false;
                }

                else if (strickerturn.LastGoti != null) //if this is last goti and queen is not potted
                {
                    strickerturn.LastGoti.GetComponent<LastGoti>().Spawn();
                    return;
                }

                AIScore = AIScore + 5;
                strickerturn.OneMoreTurn = true;
                Destroy(collision.gameObject);

            }
            else if (collision.gameObject.tag == "RGoti")
            {
               // AIScore = AIScore + 50;
                strickerturn.OneMoreTurn = true;
                strickerturn.queenScore = true;

                collision.gameObject.SetActive(false);
                redgoti = collision.gameObject;
            }

            
        }

    }

   
    }

