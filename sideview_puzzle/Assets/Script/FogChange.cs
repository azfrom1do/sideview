using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class FogChange : MonoBehaviour
{
    public Volume volume;
    private Fog fog;
    public float StartPosition;
    public float EndPosition;

    // Start is called before the first frame update
    void Start()
    {
        volume.profile.TryGet<Fog>(out fog);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ChangeFog(other.transform.position);
        }
    }
    private void ChangeFog(Vector3 PlayerPostion)
    {
        float t = (PlayerPostion.x - StartPosition) / (EndPosition - StartPosition);
        float result = Mathf.Lerp(40, 260, t);
        fog.meanFreePath.value = result;

    }
}