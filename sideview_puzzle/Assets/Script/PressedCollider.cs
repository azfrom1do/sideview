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
            player.GetComponent<PlayerMove>().Pressed = false;
            player.GetComponent<PlayerMove>().canjump = false;
            Invoke("OnInvoke",0.1f);
        }
            
    }
    void OnInvoke()
    {
        Debug.Log("1�� ��ٸ�");
        player.GetComponent<PlayerMove>().Pressed = true;
        player.GetComponent<PlayerMove>().canjump = true;
    }
   
}
