using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoxControl : MonoBehaviour
{
    private Rigidbody RB;
    GameObject player;
    Rigidbody playerRigid;
    private float boxSizeX = 2;

    public bool isGrip = false; //�ڵ� ������ �ܺο����� ��밡��
    private KeyCode moveLeft;
    private KeyCode moveRight;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerRigid = player.GetComponent<Rigidbody>();
        }
        else Debug.Log("Player Tag not found");

        //�ڽ��� x����
        if (GetComponent<BoxCollider>())
        {
            boxSizeX = GetComponent<BoxCollider>().size.x * transform.localScale.x;
        }
        else if(GetComponent<SphereCollider>())
        {
            // ��ü
            boxSizeX = GetComponent<SphereCollider>().radius * 2 * transform.localScale.x;
        }
        else
        {
            Debug.Log(gameObject.name + " BoxControl Script : BoxCollider not found");
        }

        moveLeft = player.GetComponent<PlayerMove>().moveLeft;
        moveRight = player.GetComponent<PlayerMove>().moveRight;
    }

    void FixedUpdate()
    {
        //PlayerMime();
        BoxPullPush();
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
        
        float distance = Vector3.Distance(playerRigid.position, RB.position);
        //�÷��̾���� �Ÿ�

        if (distance < boxSizeX * 0.5f + 0.8f && player.gameObject.GetComponent<PlayerMove>().canjump)
        {
            if (playerRigid.position.x > RB.position.x)
            {
                //�÷��̾ �ڽ��� �����ʿ��� �ж� �������� �̵�
                RB.transform.Translate(Vector3.left * player.gameObject.GetComponent<PlayerMove>().speed * Time.deltaTime);
            }
            if (playerRigid.position.x < RB.position.x)
            {
                //�÷��̾ �ڽ��� ���ʿ��� �ж� ���������� �̵�
                RB.transform.Translate(Vector3.right * player.gameObject.GetComponent<PlayerMove>().speed * Time.deltaTime);
            }
        }
    }

    /**�ڽ� ����*/
    private void BoxPullPush()
    {
        float distance = Vector3.Distance(playerRigid.position, RB.position);
        //�÷��̾���� �Ÿ�

        if (distance < boxSizeX * 0.5f + 0.8f && player.GetComponent<PlayerMove>().canjump && isGrip)
        {
            float playerSpeed = player.GetComponent<PlayerMove>().speed;
            if (playerRigid.position.x > RB.position.x)
            {
                // �÷��̾ �ڽ��� �����ʿ� ���� ��
                if (Input.GetKey(moveRight))
                {
                    // �÷��̾ ���������� �����̸� �ڽ��� ����
                    RB.velocity = new Vector2(playerSpeed * 1.05f, RB.velocity.y);
                }
                else if (Input.GetKey(moveLeft))
                {
                    // �÷��̾ �������� �����̸� �ڽ��� �б�
                    RB.velocity = new Vector2(-playerSpeed * 1.05f, RB.velocity.y);
                }
            }
            else if (playerRigid.position.x < RB.position.x)
            {
                // �÷��̾ �ڽ��� ���ʿ� ���� ��
                if (Input.GetKey(moveLeft))
                {
                    // �÷��̾ �������� �����̸� �ڽ��� ����
                    RB.velocity = new Vector2(-playerSpeed * 1.05f, RB.velocity.y);
                }
                else if (Input.GetKey(moveRight))
                {
                    // �÷��̾ ���������� �����̸� �ڽ��� �б�
                    RB.velocity = new Vector2(playerSpeed * 1.05f, RB.velocity.y);
                }
            }
        }
        else
        {
            // �ڽ��� ���ߵ��� �ӵ� �ʱ�ȭ
            RB.velocity = new Vector3(RB.velocity.x, RB.velocity.y, RB.velocity.z);
        }
    }
}

/* 
 * �����ۼ� : 2024.07.02
 * �������� : 2024.07.11
 * �۾��� : ������
 * 
 */