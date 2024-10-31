using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;

public class ExposureChange : MonoBehaviour
{
    public float start = 1f;
    public float end = 0.3f;
    public float duration = 30f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ChangeLightIntensity()) ;
        }
    }
    private IEnumerator ChangeLightIntensity()
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            RenderSettings.ambientIntensity = Mathf.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
