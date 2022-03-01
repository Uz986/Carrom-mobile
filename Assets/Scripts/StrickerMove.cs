using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class StrickerMove : MonoBehaviour
{
    public enum StrikerState {
        Positioning,
        Moving
    };
    public StrikerState _strikerState;
    public GameObject ray;
    public int sideIndex;
    public CarromSides[] sides;
    public CarromSides currentSide;

    public static float force;

    private Vector3 strikerPosition;

    public Rigidbody rb;

    public Transform aimTransform;
    public CameraScript cameraScript;

    void Start()
    {
        AssignSide(0);
       //rb = GetComponent<Rigidbody>();

       //strikerPosition = GameObject.FindGameObjectWithTag("Strike").transform.position;
    }


    
    public void AssignSide(int index)
    {
        currentSide = sides[index];
        PositionStriker();
    }

    void PositionStriker()
    {
        transform.position = currentSide.transform.position;
        transform.rotation = currentSide.transform.rotation;
    }

    public void ChangeState(StrikerState state)
    {
        _strikerState = state;
    }

    public void shoot() {
        ray.SetActive(false);
        ChangeState(StrikerState.Moving);
        rb.AddForce(aimTransform.forward * force);
        StartCoroutine(respawn());
    }
    private void FixedUpdate()
    {
        

        if (Input.GetKeyUp(KeyCode.Space))
        {
            ray.SetActive(false);
            ChangeState(StrikerState.Moving);
            rb.AddForce(aimTransform.forward * force);
            StartCoroutine(respawn());
        }

        if (Input.GetKey(KeyCode.A))
        {
            
            transform.position = new Vector3(transform.position.x - 3 * Time.deltaTime, transform.position.y, transform.position.z);

            if (transform.position.x < currentSide.boundLeft)
                transform.position = new Vector3(currentSide.boundLeft, transform.position.y, transform.position.z);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + 3 * Time.deltaTime, transform.position.y, transform.position.z);
            if (transform.position.x > currentSide.boundRight)
                transform.position = new Vector3(currentSide.boundRight, transform.position.y, transform.position.z);
        }

       if (Input.GetKeyUp(KeyCode.R))
       {
           ResetTransform(2);

            Quaternion position = Quaternion.Euler(0, Random.Range(100f ,270f), 0);
            transform.rotation = position;


        } 

       
    }
    IEnumerator respawn()
    {
        
        
            yield return new WaitForSeconds(3);

            ResetTransform(0);
        
         
     }

    public void ResetTransform(int playerIndex)
    {
       
        ray.SetActive(true);
        currentSide = sides[playerIndex];
        transform.position = sides[playerIndex].transform.position;
        transform.rotation = sides[playerIndex].transform.localRotation;
        cameraScript.SetPosition(playerIndex);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.Rotate(Vector3.up, -3000f * Time.deltaTime);
         
    }

   
}






