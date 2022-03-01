using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linetarget : MonoBehaviour {

    // Use this for initialization
    RaycastHit hit;
    [SerializeField]
    LineRenderer line;

    
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        int mask = ~(1 << 8); //all layers except 8th
        if (Physics.Raycast(transform.position,transform.forward, out hit,1000f,mask))
        {
            line.SetPosition(0, transform.position);
            if (hit.collider)
            {
                line.SetPosition(1, hit.point);

            //   line.SetPosition(2, hit.point +  hit.normal*-4);
              
            }
        }
            
    }
}
