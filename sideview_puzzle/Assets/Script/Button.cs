using System;
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
    public bool isDisposable = false;   //버튼 누르는 순간 끝까지 작동후 자체 파괴

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
        if (!isDisposable)
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
        else
        {
            if (isPress) DisposableButton();
        }
    }

    /* 눌려있는동안 target이 movePoint로 이동
     * 떼는 순간 다시 target이 originPoint로 복귀
     * isDisposable가 true라면 닿는 순간 다시 떨어지더라도 끝까지 movePoint로 이동 후 자체 파괴, 즉 이때는 targetBackSpeed이 몇이건 상관 없어짐
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
    private void DisposableButton()
    {
        if (Vector3.Distance(movePoint, interactObject.transform.position) <= 0.1f)
        {
            Destroy(gameObject);
        }
        interactObject.transform.Translate((movePoint - interactObject.transform.position) * targetMoveSpeed * Time.deltaTime); 
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isDisposable)
        {
            if (!interactObject) Debug.Log(gameObject.name + " Button Script : not found interact object");
            PressButton();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!isDisposable)
        {
            if (!interactObject) Debug.Log(gameObject.name + " Button Script : not found interact object");
            else ReleaseButton();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isDisposable)
        {
            if (!interactObject) Debug.Log(gameObject.name + " Button Script : not found interact object");
            else
            {
                isPress = true;
            }
        }
    }
}

/* 
 * 최초작성 : 2024.07.03
 * 변경일자 : 2024.07.10
 * 작업자 : 윤종현
 * 
 */