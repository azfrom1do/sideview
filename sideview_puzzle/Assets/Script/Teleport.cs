using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject TeleportPoint;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TP(GameObject target)
    {
        if (TeleportPoint) target.transform.position = TeleportPoint.transform.position;
        else Debug.Log("not found teleport point");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            TP(collision.gameObject);
        }
    }
}
