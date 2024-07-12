using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] public GameObject interactObject;
    [SerializeField] public Transform movePosition;
    private Vector3 movePoint;
    private Vector3 originPoint;
    public float targetMoveSpeed = 5;
    public float targetBackSpeed = 3;
    public bool disposable = false;
    private bool isPress;

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
        // 일회용(disposable)일 경우 끝까지 이동후 거의 도착했으면 자체파괴
        if (disposable)
        {
            if (isPress)
            {
                if(Vector3.Distance(movePoint, interactObject.transform.position) < 0.1f) Destroy(gameObject);
                interactObject.transform.Translate((movePoint - interactObject.transform.position) * targetMoveSpeed * Time.deltaTime);
            }
        }
        else
        {
            // 원래 위치로 복귀
            if (!isPress)
            {
                interactObject.transform.Translate((originPoint - interactObject.transform.position) * targetBackSpeed * Time.deltaTime);
            }
            // 지정 위치로 이동
            else
            {
                interactObject.transform.Translate((movePoint - interactObject.transform.position) * targetMoveSpeed * Time.deltaTime);
            }
        }
        
    }

    /* 눌려있는동안 target이 movePoint로 이동
     * 떼는 순간 다시 target이 originPoint로 복귀
     * disposable이 true일 경우 밟았다 떼도 끝까지 movePoint로 이동 후 자체파괴
     * 
     */

    /**버튼눌림(밟힘)*/
    private void PressButton()
    {
        isPress = true;

        //Debug.Log("Button Down");
    }

    /**버튼풀림(발땜)*/
    private void ReleaseButton()
    {
        isPress = false;

        //Debug.Log("Button Up");
    }

    private void OnTriggerEnter(Collider other)
    {
        // 일회용일 경우 한번만 눌림 체크
        if (disposable)
        {
            isPress = true;
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        // 일회용이 아닐경우 계속 눌림 체크
        if (!disposable)
        {
            if (!interactObject) Debug.Log(gameObject.name + " Button Script : not found interact object");
            PressButton();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // 일회용이 아닐경우 계속 떨어짐 체크
        if (!disposable)
        {
            if (!interactObject) Debug.Log(gameObject.name + " Button Script : not found interact object");
            else ReleaseButton();
        }
    }
}

/* 
 * 최초작성 : 2024.07.03
 * 변경일자 : 2024.07.11
 * 작업자 : 윤종현
 * 
 */