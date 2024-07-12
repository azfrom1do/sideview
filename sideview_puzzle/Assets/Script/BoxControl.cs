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

    public bool isGrip = false; //코드 수정후 외부에서도 사용가능
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
        //박스의 x길이

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

    /* 현재 코드는 박스 당기기만 가능함
     * 미는 것은 기본 물리충돌 이용
     * 플레이어가 가까이 있으며 바닥에 닿아있고 KeyCode.P를 누르고 있어야지 BoxPull()함수를 호출
     * 
     */

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

    /**박스 당기기*/
    private void BoxPull()
    {
        float distance = Vector3.Distance(playerRigid.position, rigidbody.position);
        //플레이어와의 거리

        if (distance < boxSizeX * 0.5f + 0.8f && player.GetComponent<PlayerMove>().canjump && isGrip)
        {
            float playerSpeed = player.GetComponent<PlayerMove>().speed;
            if (playerRigid.position.x > rigidbody.position.x)
            {
                // 플레이어가 박스의 오른쪽에서 당길 때
                if (Input.GetKey(moveRight))
                {
                    // 오른쪽으로 당기기
                    rigidbody.velocity = new Vector2(playerSpeed, rigidbody.velocity.y);
                }
            }
            if (playerRigid.position.x < rigidbody.position.x)
            {
                // 플레이어가 박스의 왼쪽에서 당길 때
                if (Input.GetKey(moveLeft))
                {
                    // 왼쪽으로 당기기
                    rigidbody.velocity = new Vector2(-playerSpeed, rigidbody.velocity.y);
                }
            }
        }
        else
        {
            // 박스가 멈추도록 속도 초기화
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, rigidbody.velocity.z);
        }
    }
}

/* 
 * 최초작성 : 2024.07.02
 * 변경일자 : 2024.07.11
 * 작업자 : 윤종현
 * 
 */