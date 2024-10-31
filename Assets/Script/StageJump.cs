using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class StageJump : MonoBehaviour
{
    public GameObject[] stageJumpingPosition;
    public GameObject target;
    void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.J))
        {
            if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) && stageJumpingPosition[1] != null){
                StartCoroutine("Jumping", 0);
            }
            else if ((Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) && stageJumpingPosition[1] != null){
                StartCoroutine("Jumping", 1);
            }
            else if ((Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) && stageJumpingPosition[2] != null){
                StartCoroutine("Jumping", 2);
            }
            else if ((Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)) && stageJumpingPosition[3] != null){
                StartCoroutine("Jumping", 3);
            }
            else if ((Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5)) && stageJumpingPosition[4] != null){
                StartCoroutine("Jumping", 4);
            }
            else if ((Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6)) && stageJumpingPosition[5] != null){
                StartCoroutine("Jumping", 5);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine("GameRestart");
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                ExitGame();
            }
        }
    }

    IEnumerator Jumping(int n)
    {
        target.GetComponent<FadeInOut>().Fade1T();
        yield return new WaitForSeconds(1f);
        target.transform.position = stageJumpingPosition[n].transform.position;
        Debug.Log("Stage Jump to " + (n+1));
    }

    IEnumerator GameRestart()
    {
        target.GetComponent<FadeInOut>().Fade1T();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Game Restart");
    }
    
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
