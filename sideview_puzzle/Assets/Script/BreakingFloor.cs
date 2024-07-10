using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] // Add Rigidbody Component
public class BreakingFloor : MonoBehaviour
{
    public Color ColorToChange = new Color(255 / 255f, 127 / 255f, 127 / 255f, 127 / 255f); //default is light red
    public float breakingTime = 1f;
    public float regenTime = 3f;

    private Vector3 originPoint;
    private Color originColor;
    private bool isFalling; //BreakingFloor끼리 닿으면 소멸해버서 조건 추가
    private bool col_player;    //미완성, 비활성화중

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

        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Rigidbody>().constraints = 
            RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        isFalling = false;
        col_player = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Player") && collision.transform.position.y > transform.position.y)
            StartCoroutine(Breaking());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Floor") && isFalling)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            //col_player = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            //col_player = false;
        }
    }

    IEnumerator Breaking()
    {
        gameObject.GetComponent<Renderer>().material.color = ColorToChange;
        transform.position += Vector3.left * 0.02f;
        for (float i = 0; i < breakingTime; i += 0.25f)
        {
            yield return new WaitForSeconds(0.125f);
            transform.position += Vector3.right * 0.04f;
            yield return new WaitForSeconds(0.125f);
            transform.position += Vector3.left * 0.04f;
        }

        isFalling = true;
        gameObject.GetComponent<Collider>().isTrigger = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;

        yield return new WaitForSeconds(regenTime);

        isFalling = false;
        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        transform.position = originPoint;
        while (col_player)
        {
            yield return new WaitForSeconds(regenTime * 0.5f);
        }
        gameObject.GetComponent<Renderer>().enabled = true;
        gameObject.GetComponent<Collider>().isTrigger = false;
        gameObject.GetComponent<Renderer>().material.color = originColor;
    }
}

/* 
 * 최초작성 : 2024.07.08
 * 변경일자 : 2024.07.10
 * 작업자 : 윤종현
 */
