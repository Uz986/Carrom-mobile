using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{


    public Ray line;
    RaycastHit hit;
    public bool canrotate = true;

    public static rotate instance;
    // Use this for initialization
    void Start()
    {
        //  transform.Rotate(Vector3.up, -0.6561228f * Time.deltaTime);


    }

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("more then one rotate instance");

        }
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P) && canrotate)
        {

            transform.Rotate(Vector3.up, 30f * Time.deltaTime);


        }
        if (Input.GetKey(KeyCode.I) && canrotate)
        {
            transform.Rotate(Vector3.down, 30f * Time.deltaTime);

        }

        if (Input.touches.Length > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && canrotate)
        {


            if (Input.touches[0].position.x > (Screen.width / 4) && Input.touches[0].position.x < ((Screen.width *4)/ 5) )
            {
                 transform.rotation = transform.rotation * Quaternion.AngleAxis(Input.GetTouch(0).deltaPosition.x, transform.up);
               // transform.Rotate(Input.GetTouch(0).deltaPosition.x, Vector3.up);
            }
        }


      
    }
}