using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRotation : MonoBehaviour {

    float speed = 1.0f;
    Transform to;

    bool playerInFront;

    float time =0;
    public float totalTime = 5.0f;

    bool opened=false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        time += Time.deltaTime;
        if (playerInFront && !opened) {
            if (Input.GetKey(KeyCode.F))
            {
                transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, new Vector3(0, 0, -110), time / totalTime);
                opened = true;
                //Vector3.Lerp(firstVector, secondVector, time / totalTime);
                //transform.localEulerAngles = new Vector3(0, 0, -110);
                //transform.Rotate(Vector3.forward * 50 * Time.deltaTime, Space.Self);
                //transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(0,0,-110), Time.time * speed);
            }
        }    
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "DrTraumothy")
        {
            playerInFront = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "DrTraumothy")
        {
            playerInFront = false;
        }
    }
}
