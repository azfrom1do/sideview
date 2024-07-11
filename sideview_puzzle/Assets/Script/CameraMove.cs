using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMove : MonoBehaviour
{
    public GameObject triggerCamera = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            triggerCamera.GetComponent<CinemachineVirtualCamera>().enabled = true;
            triggerCamera.GetComponent<CinemachineVirtualCamera>().Priority = 11;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            triggerCamera.GetComponent<CinemachineVirtualCamera>().enabled = false;
            triggerCamera.GetComponent<CinemachineVirtualCamera>().Priority = 9;
            
        }
    }
}
