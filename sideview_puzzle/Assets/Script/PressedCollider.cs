using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressedCollider : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(player.GetComponent<PlayerMove>().canjump);
        //Debug.Log(player.GetComponent<PlayerMove>().Pressed);

    }
    //�ٴۿ� ������ ���� ��������
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Floor")
        {
            Debug.Log(player.GetComponent<PlayerMove>().canjump);
            player.GetComponent<PlayerMove>().canjump = true;
            //player.GetComponent<PlayerMove>().Pressed = true;
        }
    }
    //�ٴۿ��� �������� ���� ����
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Floor")
        {
            player.GetComponent<PlayerMove>().canjump = false;
            //player.GetComponent<PlayerMove>().Pressed = false;
        }

    }
}
