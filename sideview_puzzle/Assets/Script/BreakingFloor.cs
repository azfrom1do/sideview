using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] // Add Rigidbody Component
public class BreakingFloor : MonoBehaviour
{
    public Color ColorToChange = new Color(255 / 255f, 127 / 255f, 127 / 255f, 127 / 255f); //default is light red
    public float breakingTime = 1f;
    public float regenTime = 3f;

    private Vector3 originPoint;
    private Color originColor;
    private bool isFalling; //BreakingFloor���� ������ �Ҹ��ع��� ���� �߰�
    private bool col_player;    //�̿ϼ�, ��Ȱ��ȭ��

    /* ������ ���� ��ġ�� ���� �ڽ��� ������ ���
     * �÷��̾ ������ ������ Breaking �ڷ�ƾ ȣ��
     * ������ �ٲٰ� breakingTime �ð� ���� ��鸰 ���� isKinematic�� ���� ������
     * regenTime �ð� ���� ����� �״� ��ġ�� ���� �� ���� ����
     * isKinematic �ٽ� Ȱ��ȭ
     * 
     */

    void Awake()
    {
        originPoint = transform.position;
        originColor = gameObject.GetComponent<Renderer>().material.color;

        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Rigidbody>().constraints = 
            RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        isFalling = false;
        col_player = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Player") && collision.transform.position.y > transform.position.y)
            StartCoroutine(Breaking());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Floor") && isFalling)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            //col_player = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            //col_player = false;
        }
    }

    IEnumerator Breaking()
    {
        gameObject.GetComponent<Renderer>().material.color = ColorToChange;
        transform.position += Vector3.left * 0.02f;
        for (float i = 0; i < breakingTime; i += 0.25f)
        {
            yield return new WaitForSeconds(0.125f);
            transform.position += Vector3.right * 0.04f;
            yield return new WaitForSeconds(0.125f);
            transform.position += Vector3.left * 0.04f;
        }

        isFalling = true;
        gameObject.GetComponent<Collider>().isTrigger = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;

        yield return new WaitForSeconds(regenTime);

        isFalling = false;
        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        transform.position = originPoint;
        while (col_player)
        {
            yield return new WaitForSeconds(regenTime * 0.5f);
        }
        gameObject.GetComponent<Renderer>().enabled = true;
        gameObject.GetComponent<Collider>().isTrigger = false;
        gameObject.GetComponent<Renderer>().material.color = originColor;
    }
}

/* 
 * �����ۼ� : 2024.07.08
 * �������� : 2024.07.10
 * �۾��� : ������
 */
