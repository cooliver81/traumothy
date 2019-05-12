using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public Rigidbody2D rb;

    public float speed = 3f;

    public WarPlayerController playerControl;

    public GameObject player;

    public AudioSource pew;

    public static bool isLoaded = true;

	// Use this for initialization
	void Start () {
        //transform.position = Player.transform.position + new Vector3(0f, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
        if (isLoaded)
        {
		    if (Input.GetKeyDown(KeyCode.Space))
            {
                pew.Play(0);
                playerControl.enabled = false;
                Vector2 v = transform.up * speed;  // -transform.right = left
                rb.velocity = v;  // Sets velocity to left movement
            }
        }
        else
        {
            
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("test");
        transform.position = player.transform.position;
    }

    void FixedUpdate()
    {
        
    }

}
