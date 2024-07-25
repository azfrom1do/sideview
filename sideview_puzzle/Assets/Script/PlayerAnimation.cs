using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Transform target;
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //transform.LookAt(target);
        Anim();
    }

    void Anim()
    {
        if (Input.GetKey(moveLeft))
        {
            animator.SetBool("isWalk", true);
            //target.position = transform.position + Vector3.left;
        }
        else if (Input.GetKey(moveRight))
        {
            animator.SetBool("isWalk", true);
            //target.position = transform.position + Vector3.right;
        }
        else
        {
            animator.SetBool("isWalk", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("doJump");
        }
    }
}
