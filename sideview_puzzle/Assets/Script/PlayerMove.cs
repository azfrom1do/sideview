using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public float speed = 1;
    public float jumpForce = 300f;
    private Rigidbody rigidbody;
    public bool canjump = true;
    public bool Pressed = true;

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
        if (Input.GetKey(moveLeft) && Pressed)
        {
            rigidbody.velocity = new Vector2(speed * -1, rigidbody.velocity.y);
        }
        if (Input.GetKey(moveRight) && Pressed)
        {
            rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.Space) && canjump)
        {
            rigidbody.AddForce(Vector3.up * jumpForce);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            canjump = true;
        }
    }
    //바닦에서 떨어지면 점프 못함
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            canjump = false;
        }

    }
}
