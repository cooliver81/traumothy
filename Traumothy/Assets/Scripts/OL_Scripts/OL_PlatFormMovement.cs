using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OL_PlatFormMovement : MonoBehaviour
{

    public float speed = 0.1f;
    public GameObject platform;
    

    // Use this for initialization

    // Update is called once per frame
    void FixedUpdate()
    {
        float newAxisZ = transform.position.z - speed;
        transform.position = new Vector3(transform.position.x, transform.position.y, newAxisZ);
    }
}

