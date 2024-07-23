using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class AmbientOnOff : MonoBehaviour
{
    public bool isON = true;
    public PostProcessVolume postProcessVolume; // Post-process Volume ������Ʈ
    private Collider collider;

    private void Start()
    {
        collider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isON == true)
        {
            // Post-process �������� ��������
            PostProcessProfile profile = postProcessVolume.profile;

            // Ambient Occlusion ���� ��������
            AmbientOcclusion ambientOcclusion;
            if (profile.TryGetSettings(out ambientOcclusion))
            {
                // Ambient Occlusion ��Ȱ��ȭ
                ambientOcclusion.active = false;
                isON = false;
                collider.enabled = false;
            }
        }
        else if (other.gameObject.CompareTag("Player") && isON == false)
        {
            // Post-process �������� ��������
            PostProcessProfile profile = postProcessVolume.profile;

            // Ambient Occlusion ���� ��������
            AmbientOcclusion ambientOcclusion;
            if (profile.TryGetSettings(out ambientOcclusion))
            {
                // Ambient Occlusion Ȱ��ȭ
                ambientOcclusion.active = true;
                isON = true;
                collider.enabled = false;
            }
        }
    }
}
