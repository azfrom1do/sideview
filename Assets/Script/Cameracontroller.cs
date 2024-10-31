using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 playerTransform;
    Vector3 cameraOffset = new Vector3(0, 4, -10);
    public bool canCameraController = true;
    public float duration = 0.9f;
    private bool isLerping = false;
    [SerializeField]
    private AnimationCurve moveCurve;

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (canCameraController && !isLerping)
        {
            StartCoroutine(LerpToPlayer());
        }
    }

    IEnumerator LerpToPlayer()
    {
        playerTransform = GameObject.FindWithTag("Player").transform.position;
        isLerping = true;
        float timeElapsed = 0;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = playerTransform + cameraOffset;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, duration);
            
            yield return null;
        }
        transform.position = targetPosition; // 최종 위치로 설정
        isLerping = false;

    }
}
