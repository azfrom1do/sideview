using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookSimple : MonoBehaviour {

	private float rotationY = 0f;
	private float rotationX = 0f;
	public float mouseSensitivity = 2.0f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		rotationX = Camera.main.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;
		rotationY += Input.GetAxis("Mouse Y") * mouseSensitivity;
		rotationY = Mathf.Clamp(rotationY,-50f,50f);
		Camera.main.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.anyKey) {
			transform.Translate(Vector3.forward * 100 * Time.deltaTime);
		}
	}
}
