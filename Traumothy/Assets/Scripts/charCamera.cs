﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charCamera : MonoBehaviour {

    public bool lockCursor;

    public float mouseSensitivity = 10;
    public Transform target;
    public float distanceTarget = 5;

    public Vector2 pitchMinMax = new Vector2(-40, 85); //use vector to state bounds of the pitch

    public float rotationSmoothTime = 0.12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    float yaw; //x axis
    float pitch;  //y axis
    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update () {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * distanceTarget;
    }
}
