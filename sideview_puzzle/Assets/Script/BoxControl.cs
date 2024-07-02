using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoxControl : MonoBehaviour
{
    private Rigidbody rigidbody;
    GameObject player;
    Rigidbody playerRigid;
    float boxSizeX;
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
    }

    void Update()
    {
        //PlayerMime();

    }

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
}
