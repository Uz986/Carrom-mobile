using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score   : MonoBehaviour {

    //  public GameObject Hole;
    [SerializeField]
    public static int i=0;
    public static int j;
    public Text a;
    // Use this for initialization
    void Start () {
        i = j;
    }
	
	// Update is called once per frame
	void Update () {
        a.text = "" + i.ToString();
    }
    private void OnCollisionEnter(Collision collision)
    {
            if (collision.gameObject.tag == "WGoti")
            {
                i=i+10;
                Debug.Log(i);
                Destroy(collision.gameObject);

            }
            else if (collision.gameObject.tag == "BGoti")
        {
            i = i + 5;
            Debug.Log(i);
            Destroy(collision.gameObject);

        }
        else if (collision.gameObject.tag == "RGoti")
        {
            i = i + 20;
            Debug.Log(i);
            Destroy(collision.gameObject);
        }

    }

   
    }

