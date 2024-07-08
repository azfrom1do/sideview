using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject TeleportPoint;
    public string targetTag = "Player";

    /* Collider trigger 감지시 대상을 TeleportPoint의 위치로 이동시킴
     */

    /**target을 TeleportPoint의 position으로 이동*/
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
 * 생성 : 2024.07.03
 * 변경 : 2024.07.04
 * 이름 : 윤종현
 * 
 */
