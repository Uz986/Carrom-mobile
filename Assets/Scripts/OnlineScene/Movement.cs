
using UnityEngine.Networking;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class Movement : NetworkBehaviour
{


   
    RaycastHit hit;

  

    [HideInInspector]
    public float boundleft ;
    [HideInInspector]
    public float boundright;

    void Start()
    {
        boundright = 134.39f;
        boundleft = 127.89f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.P) )
        {

            transform.Rotate(Vector3.up, 30f * Time.deltaTime);


        }
        if (Input.GetKey(KeyCode.I) )
        {
            transform.Rotate(Vector3.down, 30f * Time.deltaTime);

        }

        if (Input.touches.Length > 0 && Input.GetTouch(0).phase == TouchPhase.Moved )
        {


            if (Input.touches[0].position.x > (Screen.width / 4) && Input.touches[0].position.x < ((Screen.width *4)/ 5) )
            {
                 transform.rotation = transform.rotation * Quaternion.AngleAxis(Input.GetTouch(0).deltaPosition.x, transform.up);
               // transform.Rotate(Input.GetTouch(0).deltaPosition.x, Vector3.up);
            }
        }

        if (isServer)
        {
            if (Input.GetKey(KeyCode.A) || 0 < CrossPlatformInputManager.GetAxis("Horizontal"))
            {

                transform.position = new Vector3(transform.position.x - 3 * Time.deltaTime, transform.position.y, transform.position.z);

                if (transform.position.x < boundleft)
                    transform.position = new Vector3(boundleft, transform.position.y, transform.position.z);

                checkforspawningposition(-1);

            }
            else if (Input.GetKey(KeyCode.D) || 0 > CrossPlatformInputManager.GetAxis("Horizontal"))
            {
                transform.position = new Vector3(transform.position.x + 3 * Time.deltaTime, transform.position.y, transform.position.z);
                if (transform.position.x > boundright)
                    transform.position = new Vector3(boundright, transform.position.y, transform.position.z);

                checkforspawningposition(1);

            }
        }
        else
        {
            if (Input.GetKey(KeyCode.A) || 0 > CrossPlatformInputManager.GetAxis("Horizontal"))
            {

                transform.position = new Vector3(transform.position.x - 3 * Time.deltaTime, transform.position.y, transform.position.z);

                if (transform.position.x < boundleft)
                    transform.position = new Vector3(boundleft, transform.position.y, transform.position.z);

                checkforspawningposition(-1);

            }
            else if (Input.GetKey(KeyCode.D) || 0 < CrossPlatformInputManager.GetAxis("Horizontal"))
            {
                transform.position = new Vector3(transform.position.x + 3 * Time.deltaTime, transform.position.y, transform.position.z);
                if (transform.position.x > boundright)
                    transform.position = new Vector3(boundright, transform.position.y, transform.position.z);

                checkforspawningposition(1);

            }
        }

        



    }

    public void checkforspawningposition(int multiplier_move)
    {
      
        LayerMask mask = ~(1 << 0 | 1 << 8); //all layers except 0 and 8

        Collider[] hits = Physics.OverlapSphere(transform.position, 0.24f, mask);
       
        foreach (Collider hit in hits)
        {
            if (hit.tag == "RGoti" || hit.tag == "WGoti" || hit.tag == "BGoti")
            {
                Debug.Log("change pos");
                transform.position = new Vector3(transform.position.x + (0.25f * multiplier_move), transform.position.y, transform.position.z);

                if (multiplier_move > 0)
                {
                    if (transform.position.x > boundright)
                        transform.position = new Vector3(boundleft, transform.position.y, transform.position.z);

                }
                else
                {
                    if (transform.position.x < boundleft)
                        transform.position = new Vector3(boundright, transform.position.y, transform.position.z);

                }

                checkforspawningposition(multiplier_move);

                return;
            }
        }



    }
}