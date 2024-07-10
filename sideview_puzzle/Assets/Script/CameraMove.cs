using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject cameraController;
    public Transform cameraMovePoint; // 카메라가 이동할 목표 지점
    public float duration = 0.9f; // 카메라가 이동하는 데 걸리는 시간

    // Start is called before the first frame update
    void Start()
    {
        cameraController = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        cameraController.GetComponent<CameraController>().canCameraController = false;
        StartCoroutine(MoveCamera());
    }

    private void OnTriggerExit(Collider other)
    {
        cameraController.GetComponent<CameraController>().canCameraController = true;
    }

    private IEnumerator MoveCamera()
    {
        Vector3 startPosition = cameraController.transform.position;
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            cameraController.transform.position = Vector3.Lerp(startPosition,cameraMovePoint.position, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        cameraController.transform.position = cameraMovePoint.position; // 최종 위치로 설정
    }
}
