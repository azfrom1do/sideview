using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject interactObject;
    public GameObject movePoint;
    private Vector3 originPoint;
    public float moveSpeed = 10;
    public float backSpeed = 5;

    private bool isPress;

    void Start()
    {
        if(interactObject) originPoint = interactObject.transform.position;
        else Debug.Log(gameObject.name + " Button Script : not found interact object");

        isPress = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void PressButton(GameObject target, float speed)
    {
        //target.transform.position = movePoint.transform.position;
        target.transform.Translate((movePoint.transform.position - target.transform.position) * speed * Time.deltaTime);
        Debug.Log("Button Down");
    }
    private void ReleaseButton(GameObject target, float speed)
    {
        target.transform.position = originPoint;
        Debug.Log("Button Up");
    }

    private void OnTriggerStay(Collider other)
    {
        if(!interactObject) Debug.Log(gameObject.name + " Button Script : not found interact object");
        else if(isPress)PressButton(interactObject, moveSpeed);

        isPress = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!interactObject) Debug.Log(gameObject.name + " Button Script : not found interact object");
        else ReleaseButton(interactObject, backSpeed);

        isPress = false;
    }
}
