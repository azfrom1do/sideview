using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSystem : MonoBehaviour
{
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.R))
        {
            Death();
        }
        
    }
    public void Death()
    {
        if( 8 < PlayerPrefs.GetInt("PointNum")&& PlayerPrefs.GetInt("PointNum")< 11)
        {
            character();
        }
    }
    public void character()
    {
        playerTransform.position = new Vector3(PlayerPrefs.GetFloat("PlayerPositionX"),
                        PlayerPrefs.GetFloat("PlayerPositionY"),
                        PlayerPrefs.GetFloat("PlayerPositionZ"));
        
    }
}
