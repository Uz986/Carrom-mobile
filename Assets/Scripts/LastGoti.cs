using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastGoti : MonoBehaviour {

    float boundRight;
    float boundLeft;

    Vector3 pos;

   

   public void Spawn()
    {

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        StartCoroutine(waitforspawn());
    }

    IEnumerator waitforspawn()
    {
       

        boundRight = 134.34f;
        boundLeft = 127.85f;
        pos = new Vector3(131.122f, 0.04100001f, 204.175f);
        Rigidbody rb = transform.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(3);

        checkforspawningposition(pos);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    void checkforspawningposition(Vector3 _pos)
    {



        Collider[] hits = Physics.OverlapSphere(_pos, 0.23f);
        foreach (Collider hit in hits)
        {
            if (hit.tag == "RGoti" )
            {
                Debug.Log("change red pos");
                transform.position = new Vector3(_pos.x + (0.26f), _pos.y, _pos.z);

                if (transform.position.x > boundRight)
                    transform.position = new Vector3(boundLeft, _pos.y, _pos.z);



                checkforspawningposition(transform.position);

                return;
            }
        }

    }
}
