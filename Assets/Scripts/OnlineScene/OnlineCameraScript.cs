using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineCameraScript : MonoBehaviour {
   
     Transform target;
    public float moveSpeed = 5;

   
    public void SetPosition(GameObject _target)
    {
        target = _target.transform;

        transform.position = target.position;
        transform.rotation = target.rotation;
      
    }
}
