using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Push : MonoBehaviour
{
    public float PushForceX = 0;
    public float PushForceY = 300f;

    /* Collider 충돌시 플레이어일 경우 대상을 PushForceX, PushForceY 방향으로 addForce로 밀어냄
     */

    /**target을 PushForceX, PushForceY 방향으로 밀쳐냄*/
    private void Push_obj(GameObject target)
    {
        target.GetComponent<Rigidbody>().velocity = Vector3.zero;
        target.GetComponent<Rigidbody>().AddForce(PushForceX, PushForceY, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Push_obj(collision.gameObject);
        }
    }
}

/* 
 * 생성 : 2024.07.03
 * 변경 : 2024.07.04
 * 이름 : 윤종현
 * 
 */