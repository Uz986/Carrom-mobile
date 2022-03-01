using System.Collections;
using UnityEngine.Networking;
using UnityEngine;

public class GotiBackOnline : NetworkBehaviour {

    Vector3 midpos;
    float boundRight;
    float boundLeft;
    void Start()
    {
        midpos = new Vector3(131.122f, 0.04100001f, 204.175f);
    }
    [Command]
	public void CmdGotiBack(GameObject _goti)
    {
       // RpcGotiBack(_goti);

        Debug.Log(gameObject.name + " revive goti");

        _goti.transform.position = midpos;
        _goti.GetComponent<Rigidbody>().velocity = Vector3.zero;
        boundRight = 134.39f;
        boundLeft = 127.89f;
        checkforspawningposition(_goti);
    }
    void RpcGotiBack(GameObject _goti)
    {
       
    }

    void checkforspawningposition(GameObject _goti)
    {

        LayerMask mask = ~(1 << 0 | 1 << 8); //all layers except 0 and 8

        Collider[] hits = Physics.OverlapSphere(_goti.transform.position, 0.21f, mask);
      
        if(_goti.tag=="RGoti")
        {
            foreach (Collider hit in hits)
            {
                if (hit.tag == "WGoti" || hit.tag == "BGoti")
                {
                    Debug.Log("change RGoti");
                    _goti.transform.position = new Vector3(_goti.transform.position.x + (0.22f), _goti.transform.position.y, _goti.transform.position.z);

                    if (_goti.transform.position.x > boundRight)
                        _goti.transform.position = new Vector3(boundLeft, _goti.transform.position.y, _goti.transform.position.z);



                    checkforspawningposition(_goti);

                    return;
                }
            }
        }
        else if(_goti.tag == "WGoti")
        {
            foreach (Collider hit in hits)
            {
                if (hit.tag == "RGoti" || hit.tag == "BGoti")
                {
                    Debug.Log("change WGoti");
                    _goti.transform.position = new Vector3(_goti.transform.position.x + (0.22f), _goti.transform.position.y, _goti.transform.position.z);

                    if (_goti.transform.position.x > boundRight)
                        _goti.transform.position = new Vector3(boundLeft, _goti.transform.position.y, _goti.transform.position.z);



                    checkforspawningposition(_goti);

                    return;
                }
            }
        }
        else if(_goti.tag == "BGoti")
        {
            Debug.Log(_goti.tag);
            foreach (Collider hit in hits)
            {
                if (hit.tag == "RGoti" || hit.tag == "WGoti")
                {
                    Debug.Log("change BGoti");
                    _goti.transform.position = new Vector3(_goti.transform.position.x + (0.22f), _goti.transform.position.y, _goti.transform.position.z);

                    if (_goti.transform.position.x > boundRight)
                        _goti.transform.position = new Vector3(boundLeft, _goti.transform.position.y, _goti.transform.position.z);



                    checkforspawningposition(_goti);

                    return;
                }
            }
        }

    }
}
