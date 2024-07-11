using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSystem : MonoBehaviour
{
    private Transform playerTransform;
    public GameObject currentMap;
    public GameObject Frefab1F;
    public GameObject Frefab2F;
    public GameObject Frefab3F;
    public GameObject Frefab4F;
    public GameObject Frefab5F;
    public Transform mapPosition;
    
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
        if (0 < PlayerPrefs.GetInt("PointNum") && PlayerPrefs.GetInt("PointNum") <= 4)
        {
            currentMap = GameObject.FindWithTag("1F");
            Transform mapTransform = currentMap.transform;
            Destroy(currentMap.gameObject);
            currentMap = Instantiate(Frefab1F, mapTransform.position, mapTransform.rotation);
            character();
        }
        if (4 < PlayerPrefs.GetInt("PointNum") && PlayerPrefs.GetInt("PointNum") <= 7)
        {
            currentMap = GameObject.FindWithTag("2F");
            Transform mapTransform = currentMap.transform;
            Destroy(currentMap.gameObject);
            currentMap = Instantiate(Frefab2F, mapTransform.position, mapTransform.rotation);
            character();
        }
        if (7 < PlayerPrefs.GetInt("PointNum") && PlayerPrefs.GetInt("PointNum") <= 9)
        {
            currentMap = GameObject.FindWithTag("3F");
            Transform mapTransform = currentMap.transform;
            Destroy(currentMap.gameObject);
            currentMap = Instantiate(Frefab3F, mapTransform.position, mapTransform.rotation);
            character();
        }

        if (9 < PlayerPrefs.GetInt("PointNum")&& PlayerPrefs.GetInt("PointNum")<=11)
        {
            currentMap = GameObject.FindWithTag("4F");
            Transform mapTransform = currentMap.transform;
            Destroy(currentMap.gameObject);
            currentMap = Instantiate(Frefab4F, mapTransform.position, mapTransform.rotation);
            character();
        }

        if (11 < PlayerPrefs.GetInt("PointNum") && PlayerPrefs.GetInt("PointNum") <= 14)
        {
            currentMap = GameObject.FindWithTag("5F");
            Transform mapTransform = currentMap.transform;
            Destroy(currentMap.gameObject);
            currentMap = Instantiate(Frefab5F, mapTransform.position, mapTransform.rotation);
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
