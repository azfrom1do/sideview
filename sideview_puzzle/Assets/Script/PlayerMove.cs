using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public float speed = 1;
    public float jumpForce = 300f;
    private Rigidbody rigidbody;
    public bool canjump = false;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetKey(moveLeft))
        {
            rigidbody.velocity = new Vector2(speed*-1, rigidbody.velocity.y );
        }
        if (Input.GetKey(moveRight))
        {
            rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space)&& canjump)
        {
            canjump = false;
            rigidbody.AddForce(Vector3.up * jumpForce);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            canjump = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
    
    }
}
