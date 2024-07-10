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
    public bool isDisposable = false;   //��ư ������ ���� ������ �۵��� ��ü �ı�

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

    /* �����ִµ��� target�� movePoint�� �̵�
     * ���� ���� �ٽ� target�� originPoint�� ����
     * isDisposable�� true��� ��� ���� �ٽ� ���������� ������ movePoint�� �̵� �� ��ü �ı�, �� �̶��� targetBackSpeed�� ���̰� ��� ������
     * 
     */

    /**��ư����(����)*/
    private void PressButton()
    {
        isPress = true;

        //Debug.Log("Button Down");
    }

    /**��ưǮ��(�߶�)*/
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
 * �����ۼ� : 2024.07.03
 * �������� : 2024.07.10
 * �۾��� : ������
 * 
 */