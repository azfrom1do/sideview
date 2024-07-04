using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    public float PushForceX = 0;
    public float PushForceY = 300f;

    /* Collider �浹�� �÷��̾��� ��� ����� PushForceX, PushForceY �������� addForce�� �о
     */

    /**target�� PushForceX, PushForceY �������� ���ĳ�*/
    private void Push_obj(GameObject target)
    {
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
 * ���� : 2024.07.23
 * ���� : 2024.07.24
 * �̸� : ������
 * 
 */