using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject interactObject;
    public GameObject movePoint;
    private Vector3 originPoint;
    void Start()
    {
        if(interactObject) originPoint = interactObject.transform.position;
        else Debug.Log(gameObject.name + " Button Script : not found interact object");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void PressButton(GameObject target, float moveSpeed)
    {
        target.transform.position = movePoint.transform.position;
        Debug.Log("Button Down");
    }
    private void ReleaseButton(GameObject target, float moveSpeed)
    {
        target.transform.position = originPoint;
        Debug.Log("Button Up");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!interactObject) Debug.Log(gameObject.name + " Button Script : not found interact object");
        else PressButton(interactObject, 20);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (!interactObject) Debug.Log(gameObject.name + " Button Script : not found interact object");
        else ReleaseButton(interactObject, 0);
    }
}
