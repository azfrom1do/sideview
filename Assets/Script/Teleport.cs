using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject TeleportPoint;
    public string targetTag = "Player";

    /* Collider trigger 감지시 대상을 TeleportPoint의 위치로 이동시킴
     */


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(targetTag))
        {
            StartCoroutine(TP(other.gameObject));
        }
    }
    public IEnumerator TP(GameObject player)
    {
        player.GetComponent<FadeInOut>().Fade4T();
        yield return new WaitForSeconds(1f);
        player.transform.position = TeleportPoint.transform.position;
    }
}

/* 
 * 생성 : 2024.07.03
 * 변경 : 2024.07.04
 * 이름 : 윤종현
 * 
 */
