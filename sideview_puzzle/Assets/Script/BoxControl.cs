using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoxControl : MonoBehaviour
{
    private Rigidbody rigidbody;
    GameObject player;
    Rigidbody playerRigid;
    float boxSizeX;

    public bool isGrip = false; //�ڵ� ������ �ܺο����� ��밡��
    private KeyCode moveLeft;
    private KeyCode moveRight;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerRigid = player.GetComponent<Rigidbody>();
        }
        else Debug.Log("Player Tag not found");

        boxSizeX = GetComponent<BoxCollider>().size.x * transform.localScale.x;
        //�ڽ��� x����

        moveLeft = player.GetComponent<PlayerMove>().moveLeft;
        moveRight = player.GetComponent<PlayerMove>().moveRight;
    }

    void FixedUpdate()
    {
        //PlayerMime();
        BoxPull();
        if (Input.GetKey(KeyCode.P))
        {
            isGrip = true;
        }
        else isGrip = false;
    }

    /* ���� �ڵ�� �ڽ� ���⸸ ������
     * �̴� ���� �⺻ �����浹 �̿�
     * �÷��̾ ������ ������ �ٴڿ� ����ְ� KeyCode.P�� ������ �־���� BoxPull()�Լ��� ȣ��
     * 
     */

    //�÷��̾���� ��ȣ�ۿ�
    private void PlayerMime()
    {
        
        float distance = Vector3.Distance(playerRigid.position, rigidbody.position);
        //�÷��̾���� �Ÿ�

        if (distance < boxSizeX * 0.5f + 0.8f && player.gameObject.GetComponent<PlayerMove>().canjump)
        {
            if (playerRigid.position.x > rigidbody.position.x)
            {
                //�÷��̾ �ڽ��� �����ʿ��� �ж� �������� �̵�
                rigidbody.transform.Translate(Vector3.left * player.gameObject.GetComponent<PlayerMove>().speed * Time.deltaTime);
            }
            if (playerRigid.position.x < rigidbody.position.x)
            {
                //�÷��̾ �ڽ��� ���ʿ��� �ж� ���������� �̵�
                rigidbody.transform.Translate(Vector3.right * player.gameObject.GetComponent<PlayerMove>().speed * Time.deltaTime);
            }
        }
    }

    /**�ڽ� ����*/
    private void BoxPull()
    {
        float distance = Vector3.Distance(playerRigid.position, rigidbody.position);
        //�÷��̾���� �Ÿ�

        if (distance < boxSizeX * 0.5f + 0.8f && player.GetComponent<PlayerMove>().canjump && isGrip)
        {
            float playerSpeed = player.GetComponent<PlayerMove>().speed;
            if (playerRigid.position.x > rigidbody.position.x)
            {
                // �÷��̾ �ڽ��� �����ʿ��� ��� ��
                if (Input.GetKey(moveRight))
                {
                    // ���������� ����
                    rigidbody.velocity = new Vector2(playerSpeed, rigidbody.velocity.y);
                }
            }
            if (playerRigid.position.x < rigidbody.position.x)
            {
                // �÷��̾ �ڽ��� ���ʿ��� ��� ��
                if (Input.GetKey(moveLeft))
                {
                    // �������� ����
                    rigidbody.velocity = new Vector2(-playerSpeed, rigidbody.velocity.y);
                }
            }
        }
        else
        {
            // �ڽ��� ���ߵ��� �ӵ� �ʱ�ȭ
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, rigidbody.velocity.z);
        }
    }
}

/* 
 * �����ۼ� : 2024.07.02
 * �������� : 2024.07.11
 * �۾��� : ������
 * 
 */