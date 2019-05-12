using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerp : MonoBehaviour {

    public GameObject TargetPosition;
    public Camera cameraTransform;

    public float newX = 0, newY = 3.18f, newZ = 1.46f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        cameraTransform.transform.position = new Vector3(Mathf.Lerp(cameraTransform.transform.position.x, TargetPosition.transform.position.x + newX, 5.0f * Time.deltaTime), Mathf.Lerp(cameraTransform.transform.position.y, TargetPosition.transform.position.y + newY, 5.0f * Time.deltaTime), Mathf.Lerp(cameraTransform.transform.position.z, TargetPosition.transform.position.z + newZ, 5.0f * Time.deltaTime));
    }
}
