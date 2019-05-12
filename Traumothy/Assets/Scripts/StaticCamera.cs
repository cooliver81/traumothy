using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCamera : MonoBehaviour {

    public Transform target;
    public float distanceTarget = 3;

    // Use this for initialization
    void Start () {
        transform.Rotate(50, 0, 0, Space.World);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = target.position - transform.forward * distanceTarget;
    }
}
