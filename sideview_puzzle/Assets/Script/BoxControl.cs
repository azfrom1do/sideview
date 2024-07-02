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
        //박스의 x길이
    }

    void Update()
    {
        //PlayerMime();

    }

    //플레이어와의 상호작용
    private void PlayerMime()
    {
        float distance = Vector3.Distance(playerRigid.position, rigidbody.position);
        //플레이어와의 거리

        if (distance < boxSizeX * 0.5f + 0.8f && player.gameObject.GetComponent<PlayerMove>().canjump)
        {
            if (playerRigid.position.x > rigidbody.position.x)
            {
                //플레이어가 박스의 오른쪽에서 밀때 왼쪽으로 이동
                rigidbody.transform.Translate(Vector3.left * player.gameObject.GetComponent<PlayerMove>().speed * Time.deltaTime);
            }
            if (playerRigid.position.x < rigidbody.position.x)
            {
                //플레이어가 박스의 왼쪽에서 밀때 오른쪽으로 이동
                rigidbody.transform.Translate(Vector3.right * player.gameObject.GetComponent<PlayerMove>().speed * Time.deltaTime);
            }
        }
    }
}
