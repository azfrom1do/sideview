using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSystem : MonoBehaviour
{
    public GameObject player; 
    private Transform playerTransform;
    public GameObject currentMap;
    public GameObject Frefab1F;
    public GameObject Frefab2F;
    public GameObject Frefab3F;
    public GameObject Frefab4F;
    public GameObject Frefab5F;
    public GameObject Frefab6F;
    public Transform mapPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Timer());
        }
        
    }
    public void Death()
    {
        if (0 < PlayerPrefs.GetInt("PointNum") && PlayerPrefs.GetInt("PointNum") <= 3)
        {
            currentMap = GameObject.FindWithTag("1F");
            Transform mapTransform = currentMap.transform;
            Destroy(currentMap.gameObject);
            currentMap = Instantiate(Frefab1F, mapTransform.position, mapTransform.rotation);
            character();
        }
        if (3 < PlayerPrefs.GetInt("PointNum") && PlayerPrefs.GetInt("PointNum") <= 5)
        {
            currentMap = GameObject.FindWithTag("2F");
            Transform mapTransform = currentMap.transform;
            Destroy(currentMap.gameObject);
            currentMap = Instantiate(Frefab2F, mapTransform.position, mapTransform.rotation);
            character();
        }
        if (5 < PlayerPrefs.GetInt("PointNum") && PlayerPrefs.GetInt("PointNum") <= 7)
        {
            currentMap = GameObject.FindWithTag("3F");
            Transform mapTransform = currentMap.transform;
            Destroy(currentMap.gameObject);
            currentMap = Instantiate(Frefab3F, mapTransform.position, mapTransform.rotation);
            character();
        }

        if (10 < PlayerPrefs.GetInt("PointNum")&& PlayerPrefs.GetInt("PointNum")<=13)
        {
            currentMap = GameObject.FindWithTag("4F");
            Transform mapTransform = currentMap.transform;
            Destroy(currentMap.gameObject);
            currentMap = Instantiate(Frefab4F, mapTransform.position, mapTransform.rotation);

            character();
        }

        if (7 < PlayerPrefs.GetInt("PointNum") && PlayerPrefs.GetInt("PointNum") <= 9)
        {
            currentMap = GameObject.FindWithTag("5F");
            Transform mapTransform = currentMap.transform;
            Destroy(currentMap.gameObject);
            currentMap = Instantiate(Frefab5F, mapTransform.position, mapTransform.rotation);
            character();
        }
        if (9 < PlayerPrefs.GetInt("PointNum") && PlayerPrefs.GetInt("PointNum") <= 10)
        {
            currentMap = GameObject.FindWithTag("6F");
            Transform mapTransform = currentMap.transform;
            Destroy(currentMap.gameObject);
            currentMap = Instantiate(Frefab6F, mapTransform.position, mapTransform.rotation);
            character();
        }
    }
    public void character()
    {
        playerTransform.position = new Vector3(PlayerPrefs.GetFloat("PlayerPositionX"),
                        PlayerPrefs.GetFloat("PlayerPositionY"),
                        PlayerPrefs.GetFloat("PlayerPositionZ"));
    }
    public IEnumerator Timer()
    {
        player.GetComponent<FadeInOut>().Fade1T();
        yield return new WaitForSeconds(1f);
        Death();
    }
}
