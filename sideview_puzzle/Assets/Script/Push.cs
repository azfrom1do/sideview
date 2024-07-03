using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    public float PushForceX = 0;
    public float PushForceY = 300f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Push_obj(GameObject target)
    {
        target.GetComponent<Rigidbody>().AddForce(PushForceX, PushForceY, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Push_obj(collision.gameObject);
        }
    }
}
