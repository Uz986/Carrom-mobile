using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenScore : MonoBehaviour {

    float boundRight;
    float boundLeft;

    Vector3 RedSpawn;

    void Awake()
    {
        RedSpawn = transform.position;
    }

    void OnEnable()
    {
        boundRight = 134.34f;
        boundLeft=127.85f;
        transform.position = RedSpawn;
        Rigidbody rb = transform.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
       

        checkforspawningposition();
    }

    void checkforspawningposition()
    {


     
        Collider[] hits = Physics.OverlapSphere(transform.position, 0.21f);
        foreach (Collider hit in hits)
        {
            if ( hit.tag == "WGoti" || hit.tag == "BGoti")
            {
                Debug.Log("change red pos");
                transform.position = new Vector3(transform.position.x + (0.22f), transform.position.y, transform.position.z);

                if (transform.position.x > boundRight)
                    transform.position = new Vector3(boundLeft, transform.position.y, transform.position.z);



                checkforspawningposition();

                return;
            }
        }

    }
}
