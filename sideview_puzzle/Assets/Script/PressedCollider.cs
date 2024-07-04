using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressedCollider : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {

    }
    //πŸ¥€ø° ¥Í¿∏∏È ¡°«¡ ∞°¥…«ÿ¡¸
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Floor")
        {
        player.GetComponent<PlayerMove>().canjump = true;
        }   
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Floor")
        {
        player.GetComponent<PlayerMove>().canjump = false;
            
        }
            
    }
}
