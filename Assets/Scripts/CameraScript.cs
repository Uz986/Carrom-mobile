using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public Transform[] camPositions;
    public Transform target;
    public float moveSpeed = 5;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation , target.rotation , moveSpeed * Time.deltaTime);
    }
    public void SetPosition(int index)
    {
        target = camPositions[index];
    }
}
