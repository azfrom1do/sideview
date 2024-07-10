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
        if (!isPress)
        {
            interactObject.transform.Translate((originPoint - interactObject.transform.position) * targetBackSpeed * Time.deltaTime);
        }
        else
        {
            interactObject.transform.Translate((movePoint - interactObject.transform.position) * targetMoveSpeed * Time.deltaTime);
        }
    }

    /* �����ִµ��� target�� movePoint�� �̵�
     * ���� ���� �ٽ� target�� originPoint�� ����
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
 * �����ۼ� : 2024.07.03
 * �������� : 2024.07.08
 * �۾��� : ������
 * 
 */