using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform PlayerTransform; 
    Vector3 cameraPosition = new Vector3(0, 4, -10);
    public bool canCameraController = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {
        if(canCameraController == true)
        {
        transform.position = Vector3.Lerp(transform.position, new Vector3(PlayerTransform.position.x, PlayerTransform.position.y-0.85f, PlayerTransform.position.z)+ cameraPosition, Time.deltaTime * 0.9f);
        }
        
    }
}
