using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Transform target;
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public GameObject player;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.LookAt(target);
        Anim();
    }

    void Anim()
    {
        if (Input.GetKey(moveLeft))
        {
            animator.SetBool("isWalk", true);
            if(!animator.GetBool("isPush")) target.position = transform.position + Vector3.left;
        }
        else if (Input.GetKey(moveRight))
        {
            animator.SetBool("isWalk", true);
            if (!animator.GetBool("isPush")) target.position = transform.position + Vector3.right;
        }
        else
        {
            animator.SetBool("isWalk", false);
        }

        if (!player.GetComponent<PlayerMove>().canjump)
        {
            animator.SetBool("isJump", true);
        }
        if (player.GetComponent<PlayerMove>().canjump)
        {
            animator.SetBool("isJump", false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Floor"))
        {
            if(Input.GetKey(KeyCode.P)) animator.SetBool("isPush", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("isPush", false);
    }
}
