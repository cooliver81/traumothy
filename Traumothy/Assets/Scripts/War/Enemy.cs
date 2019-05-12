using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {

    public Vector2 startForce = new Vector2(1f, 1f);
    public GameManager gm;
    public Camera cam;

    public float speed = 3f;

    public int counter = 0;

    public Rigidbody2D rb;

    // Use this for initialization
    void Start() {
        transform.position = new Vector2(-0.55f, 1.22f);
        startForce = new Vector2((Random.Range(0, 2) * 2 - 1) * speed, (Random.Range(0, 2) * 2 - 1) * speed);
        rb.AddForce(startForce, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update() {
        if (GameManager.paused)
        {

            GameManager.mainCam.enabled = false;
        }
        else
        {
            GameManager.mainCam.enabled = true;
            cam.enabled = false;
        }

        transform.Rotate(0, 0, 100 * Time.deltaTime);
        Debug.Log(speed);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9 )
        {
            if (counter > 3)
            {
                GameManager.patientRescued++;
                GameManager.paused = false;
                GameManager.mainCam.enabled = true;
                gm.ResumeMainGame(2);
            }
            speed++;
            counter++;
            transform.position = new Vector2(0f, 0f);
            Start();
        }
        if (collision.gameObject.layer == 11)
            Start();
    }


}
