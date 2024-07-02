using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    public KeyCode moveLeft = KeyCode.LeftArrow;
    public KeyCode moveRIght = KeyCode.RightArrow;
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
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(moveRIght))
        { 
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space)&& canjump)
        {
            canjump = false;
            rigidbody.AddForce(Vector3.up * jumpForce);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            canjump = true;
        }
    }
}
