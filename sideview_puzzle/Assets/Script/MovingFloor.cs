using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UIElements;

public class MovingFloor : MonoBehaviour
{
    public Transform FirstPoint;
    public Transform SecondPoint;
    private Vector3 point1;
    private Vector3 point2;
    public float speed = 1f;

    private bool toggle = true;
    private List<GameObject> objects = new List<GameObject>();

    /* �� ������ �պ�
     * ������ Awake�� �̵��� ��ġ�� �ʱ�ȭ �ϱ⿡ �÷��� ���� ������ ������ �� ����
     * point1 �������� ���� �̵�
     * ������ y���� ����� y�� ���ƾ� �̵���Ŵ (���� �ö�ź ���)
     * ���� speed�� �� 3�̻��� �Ǹ� �÷��̾ �ݴ������� �̵��� �� �����Ÿ��� ����
     */

    void Awake()
    {
        if (FirstPoint && SecondPoint)
        {
            point1 = FirstPoint.position;
            point2 = SecondPoint.position;
        }
        else
        {
            Debug.Log(gameObject.name + " Button Script : Transform Point is empty");
        }
    }

    private void Update()
    {
        Vector3 targetPosition = toggle ? point1 : point2;
        Vector3 direction = (targetPosition - transform.position).normalized;
        float distance = Vector3.Distance(targetPosition, transform.position);

        if (distance > 0.1f)
        {
            transform.Translate(direction * speed * Time.deltaTime);

            foreach (GameObject obj in objects)
            {
                obj.transform.Translate(direction * speed * Time.deltaTime);
            }
        }
        else
        {
            toggle = !toggle;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!objects.Contains(collision.gameObject) && collision.transform.position.y>transform.position.y)
        {
            objects.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (objects.Contains(collision.gameObject))
        {
            objects.Remove(collision.gameObject);
        }
    }
}

/* 
 * �����ۼ� : 2024.07.08
 * �������� : 2024.07.08
 * �۾��� : ������
 */
