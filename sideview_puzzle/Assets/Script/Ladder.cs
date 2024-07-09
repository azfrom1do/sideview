using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public GameObject player;
    public bool isLadder = false;
    //public bool WaitTime = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(isLadder && Input.GetKey(KeyCode.W))
        {
            player.transform.Translate(Vector3.up * 0.01f);
            //StartCoroutine(Breaking());
        }
        else if (isLadder && Input.GetKey(KeyCode.S))
        {
            player.transform.Translate(Vector3.down * 0.01f);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ladder") 
        {
            player.GetComponent<Rigidbody>().useGravity = false;
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            isLadder = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            isLadder = false;
            player.GetComponent<Rigidbody>().useGravity = true;
        }
    }
    /*IEnumerator Breaking()
    {
        WaitTime = false;
        yield return new WaitForSeconds(0.1f);
        WaitTime = true;
    }*/
}
