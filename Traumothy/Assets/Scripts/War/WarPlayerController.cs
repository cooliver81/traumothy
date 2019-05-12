using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarPlayerController : MonoBehaviour {

    public float speed = 4f;

    public Rigidbody2D rb;

    private float movement = 0f;

    public int lives = 3;

    public GameObject health1;
    public GameObject health2;
    public GameObject health3;

    public AudioSource explosion;

    // Use this for initialization
    void Start () {
      
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal") * speed;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + new Vector2 (movement * Time.fixedDeltaTime, 0f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( this.gameObject.layer == 11 && collision.gameObject.layer == 12) {
            lives--;
            explosion.Play(0);
            switch (lives)
            {
                case 2:
                    health1.SetActive(false);
                    break;
                case 1:
                    health2.SetActive(false);
                    break;
                case 0:
                    health3.SetActive(false);
                    break;
            }
        }

        if (lives <= 0)
        {
            SceneManager.LoadScene("War");
        }
        Debug.Log(lives);

        Start();
    }

}
