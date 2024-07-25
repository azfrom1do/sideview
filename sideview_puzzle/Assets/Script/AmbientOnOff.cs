using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class AmbientOnOff : MonoBehaviour
{
    public bool isON = true;
    public PostProcessVolume postProcessVolume; // Post-process Volume 컴포넌트
    private Collider collider;

    private void Start()
    {
        collider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isON == true)
        {
            // Post-process 프로파일 가져오기
            PostProcessProfile profile = postProcessVolume.profile;

            // Ambient Occlusion 설정 가져오기
            AmbientOcclusion ambientOcclusion;
            if (profile.TryGetSettings(out ambientOcclusion))
            {
                // Ambient Occlusion 비활성화
                ambientOcclusion.active = false;
                isON = false;
                collider.enabled = false;
            }
        }
        else if (other.gameObject.CompareTag("Player") && isON == false)
        {
            // Post-process 프로파일 가져오기
            PostProcessProfile profile = postProcessVolume.profile;

            // Ambient Occlusion 설정 가져오기
            AmbientOcclusion ambientOcclusion;
            if (profile.TryGetSettings(out ambientOcclusion))
            {
                // Ambient Occlusion 활성화
                ambientOcclusion.active = true;
                isON = true;
                collider.enabled = false;
            }
        }
    }

}
