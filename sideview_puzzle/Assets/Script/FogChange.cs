using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;

public class FogColor : MonoBehaviour
{
    public GameObject Camera;
    public Collider OffCollider;
    public Material newSkybox;
    public Light sceneLight;
    public float rotationAngle = -60f;
    public float duration = 60f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            RenderSettings.skybox = newSkybox;
            RenderSettings.fog = false;
            StartCoroutine(ChangeLightColor(sceneLight.color, rotationAngle)) ;
        }
    }
    private IEnumerator ChangeLightColor(Color startColor, float rotationAngle)
    {
        Quaternion startRotation = sceneLight.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(rotationAngle, 0, 0);
        float elapsed = 0f;
        while (elapsed < duration)
        {
            sceneLight.transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        sceneLight.transform.rotation = endRotation;
        OffCollider.enabled = false;
    }
}
