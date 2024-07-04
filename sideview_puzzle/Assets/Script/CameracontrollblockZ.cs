using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllblockZ : MonoBehaviour
{
    public Transform PlayerTransform; 
    Vector3 cameraPosition = new Vector3(0, 4, -10);
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame

    void FixedUpdate()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            player.GetComponent<CameraController>().canCameraController = false;
        }
    }
}
