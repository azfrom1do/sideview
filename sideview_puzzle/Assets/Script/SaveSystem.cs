using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{ 
    public int savePointNum;
    public BoxCollider boxcollider;
    // Start is called before the first frame update
    void Start()
    {
        boxcollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("PointNum",  savePointNum);
            PlayerPrefs.SetFloat("PlayerPositionX",other.transform.position.x );
            PlayerPrefs.SetFloat("PlayerPositionY", other.transform.position.y);
            PlayerPrefs.SetFloat("PlayerPositionZ", other.transform.position.z);
            boxcollider.enabled = false;
        }
    }

}
