using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] // Add Rigidbody Component
public class BreakingFloor : MonoBehaviour
{
    public Color ColorToChange = new Color(255 / 255f, 127 / 255f, 127 / 255f, 127 / 255f); //default is light red
    public float breakingTime = 1f; //n초 뒤에 떨어짐
    public float regenTime = 3f;    //떨어지고 n초 뒤에 재생성

    private Vector3 originPoint;
    private Color originColor;
    private bool isFalling;     //BreakingFloor끼리 닿으면 소멸해버려서 조건 추가
    private bool col_player;    //플레이어와 겹쳐있으면 리젠되지 않음

    /* 생성시 현재 위치와 현재 자신의 색상을 기억
     * 플레이어가 위에서 닿으면 Breaking 코루틴 호출
     * 색상을 바꾸고 breakingTime 시간 동안 흔들린 다음 isKinematic을 끄고 떨어짐
     * regenTime 시간 이후 기억해 뒀던 위치로 복귀 및 색상 복구
     * isKinematic 다시 활성화
     * 
     */

    void Awake()
    {
        originPoint = transform.position;
        originColor = gameObject.GetComponent<Renderer>().material.color;

        // Rigidbody 설정
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Rigidbody>().constraints = 
            RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

        // bool변수 초기화
        isFalling = false;
        col_player = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 플레이어가 위에 닿았을때
        if(collision.gameObject.tag.Equals("Player") && collision.transform.position.y > transform.position.y)
            StartCoroutine(Breaking());
    }

    private void OnTriggerEnter(Collider other)
    {
        // 떨어질때 바닥에 닿으면 투명화
        if (other.gameObject.tag.Equals("Floor") && isFalling)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        // 플레이어와 겹쳐 있으면 true
        if (other.gameObject.tag.Equals("Player"))
        {
            col_player = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            col_player = false;
        }
    }
    private void FixedUpdate()
    {
        // 간혹 생명주기 텀에 플레이어가 벗어나면 OnTriggerExit 감지를 못함, FixedUpdata로 물리판정과 동시에 검사, Exit에 탐지 못한경우의 대처
        if (col_player)
        {
            col_player = false;
        }
    }

    /**낙하 코루틴*/
    IEnumerator Breaking()
    {
        // 설정 색상으로 변경
        gameObject.GetComponent<Renderer>().material.color = ColorToChange;

        // 좌우로 흔들림
        transform.position += Vector3.left * 0.02f;
        for (float i = 0; i < breakingTime; i += 0.25f)
        {
            yield return new WaitForSeconds(0.125f);
            transform.position += Vector3.right * 0.04f;
            yield return new WaitForSeconds(0.125f);
            transform.position += Vector3.left * 0.04f;
        }

        // 떨어짐
        isFalling = true;
        gameObject.GetComponent<Collider>().isTrigger = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;

        yield return new WaitForSeconds(regenTime-0.5f);

        // 설정 시간이 흐르고 복귀
        isFalling = false;
        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Renderer>().material.color = originColor;
        transform.position = originPoint;

        // 플레이어가 겹쳐있다면 비킬때까지 기다림
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !col_player);

        // 투명화 해제
        gameObject.GetComponent<Renderer>().enabled = true;
        gameObject.GetComponent<Collider>().isTrigger = false;
    }
}

/* 
 * 최초작성 : 2024.07.08
 * 변경일자 : 2024.07.11
 * 작업자 : 윤종현
 */
