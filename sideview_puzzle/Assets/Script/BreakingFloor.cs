using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BreakingFloor : MonoBehaviour
{
    public Color ColorToChange = new Color(255 / 255f, 127 / 255f, 127 / 255f); //default is light red
    public float breakingTime = 1f;
    public float regenTime = 3f;

    private Vector3 originPoint;
    private Color originColor;

    /* 생성시 현재 위치와 현재 자신의 색상을 기억
     * 플레이어가 위에서 닿으면 Breaking 코루틴 호출
     * 색상을 바꾸고 breakingTime 시간 동안 흔들린 다음 isKinematic을 끄고 떨어짐
     * regenTime 시간 이후 기억해 뒀던 위치로 복귀 및 색상 복구
     * isKinematic 다시 활성화
     */

    void Awake()
    {
        originPoint = transform.position;
        originColor = gameObject.GetComponent<Renderer>().material.color;

        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Player") && collision.transform.position.y > transform.position.y)
            StartCoroutine(Breaking());
        if (collision.gameObject.tag.Equals("Floor"))
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
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

        gameObject.GetComponent<Rigidbody>().isKinematic = false;

        yield return new WaitForSeconds(regenTime);

        gameObject.GetComponent<Renderer>().enabled = true;
        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        transform.position = originPoint;
        gameObject.GetComponent<Renderer>().material.color = originColor;
    }
}

/* 
 * 최초작성 : 2024.07.08
 * 변경일자 : 2024.07.08
 * 작업자 : 윤종현
 */
