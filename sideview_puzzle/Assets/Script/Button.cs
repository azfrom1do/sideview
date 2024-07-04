using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Button : MonoBehaviour
{
    public GameObject interactObject;
    public Transform movePosition;
    private Vector3 movePoint;
    private Vector3 originPoint;
    public float targetMoveSpeed = 5;
    public float targetBackSpeed = 3;

    private bool isPress;
    private Rigidbody interactObjectRigid;
    void Awake()
    {
        if (interactObject && movePosition)
        {
            movePoint = movePosition.position;
            originPoint = interactObject.transform.position;
        }
        else Debug.Log(gameObject.name + " Button Script : not found interactObject or movePosition");

        isPress = false;
    }

    private void Update()
    {
        if (!isPress)
        {
            interactObject.transform.Translate((originPoint - interactObject.transform.position) * targetBackSpeed * Time.deltaTime);
        }
        else
        {
            interactObject.transform.Translate((movePoint - interactObject.transform.position) * targetMoveSpeed * Time.deltaTime);
        }
    }

    /* 눌려있는동안 target이 movePoint로 이동
     * 떼는 순간 다시 target이 originPoint로 복귀
     */

    /**버튼눌림(밟힘)*/
    private void PressButton()
    {
        isPress = true;

        Debug.Log("Button Down");
    }

    /**버튼풀림(발땜)*/
    private void ReleaseButton()
    {
        isPress = false;

        Debug.Log("Button Up");
    }

    private void OnTriggerStay(Collider other)
    {
        if (!interactObject) Debug.Log(gameObject.name + " Button Script : not found interact object");
        PressButton();
    }
    private void OnTriggerExit(Collider other)
    {
        if (!interactObject) Debug.Log(gameObject.name + " Button Script : not found interact object");
        else ReleaseButton();
    }
}

/* 
 * 최초작성 : 2024.07.23
 * 변경일자 : 2024.07.24
 * 작업자 : 윤종현
 * 
 */