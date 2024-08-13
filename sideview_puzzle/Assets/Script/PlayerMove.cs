using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public float speed = 3;
    public float jumpForce = 300f;
    private Rigidbody RB;
    public bool canjump = true;
    public bool Pressed = true;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
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
            RB.velocity = new Vector2(speed * -1, RB.velocity.y);
        }
        if (Input.GetKey(moveRight) && Pressed)
        {
            RB.velocity = new Vector2(speed, RB.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.Space) && canjump)
        {
            RB.AddForce(Vector3.up * jumpForce);
        }
    }
}
