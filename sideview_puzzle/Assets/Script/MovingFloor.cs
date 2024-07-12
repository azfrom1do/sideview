using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UIElements;

public class MovingFloor : MonoBehaviour
{
    public Transform FirstPoint;
    public Transform SecondPoint;
    private Vector3 point1;
    private Vector3 point2;
    public float speed = 1f;

    private bool toggle = true;
    private List<GameObject> objects = new List<GameObject>();

    /* 두 지점을 왕복
     * 생성시 Awake로 이동할 위치를 초기화 하기에 플레이 도중 지점을 변경할 수 없음
     * point1 방향으로 먼저 이동
     * 본인의 y보다 대상의 y가 높아야 이동시킴 (위에 올라탄 대상만)
     * 현재 speed가 약 3이상이 되면 플레이어가 반대쪽으로 이동할 때 버벅거림이 있음
     * 
     */

    void Awake()
    {
        if (FirstPoint && SecondPoint)
        {
            point1 = FirstPoint.position;
            point2 = SecondPoint.position;
        }
        else
        {
            Debug.Log(gameObject.name + " Button Script : Transform Point is empty");
        }
    }

    private void Update()
    {
        Vector3 targetPosition = toggle ? point1 : point2;
        Vector3 direction = (targetPosition - transform.position).normalized;
        float distance = Vector3.Distance(targetPosition, transform.position);

        if (distance > 0.1f)
        {
            transform.Translate(direction * speed * Time.deltaTime);

            foreach (GameObject obj in objects)
            {
                obj.transform.Translate(direction * speed * Time.deltaTime);
            }
        }
        else
        {
            toggle = !toggle;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!objects.Contains(collision.gameObject) && collision.transform.position.y>transform.position.y)
        {
            objects.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (objects.Contains(collision.gameObject))
        {
            objects.Remove(collision.gameObject);
        }
    }
}

/* 
 * 최초작성 : 2024.07.08
 * 변경일자 : 2024.07.08
 * 작업자 : 윤종현
 */
