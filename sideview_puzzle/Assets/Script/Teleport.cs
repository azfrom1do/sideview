using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject TeleportPoint;
    public string targetTag = "Player";

    /* Collider trigger ������ ����� TeleportPoint�� ��ġ�� �̵���Ŵ
     */

    /**target�� TeleportPoint�� position���� �̵�*/
    private void TP(GameObject target)
    {
        if (TeleportPoint) target.transform.position = TeleportPoint.transform.position;
        else Debug.Log("not found teleport point");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(targetTag))
        {
            TP(other.gameObject);
        }
    }
}

/* 
 * ���� : 2024.07.03
 * ���� : 2024.07.04
 * �̸� : ������
 * 
 */
