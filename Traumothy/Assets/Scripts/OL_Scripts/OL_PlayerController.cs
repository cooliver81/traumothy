using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OL_PlayerController : MonoBehaviour {

    public Vector3 offset;
    public float speed = 10f;

    public GameManager gm;
    public Camera cam;

    public GameObject player;
    public GameObject livesIcon;
    public OL_PlatFormMovement platformMov;
    public Text livesText;

    public float blinkingSpeed = 0.1f;
    public int lives = 3;

    float timer = 0f;
    private bool isTicking = false;
    private bool isHit = false;
    private bool isDead = false;

    public AudioSource smoochHurt;

    // Use this for initialization
    void Start () {
       
    }

    private void Update()
    {
        if (GameManager.paused)
        {

            GameManager.mainCam.enabled = false;
        }
        else
        {
            GameManager.mainCam.enabled = true;
            cam.enabled = false;
        }

        checkLives();
        //Bug fix
        if (offset.x == -3.08f)
            offset.x = -3;
        else if (offset.x == 3.08f)
            offset.x = 3;

        if(isTicking)
            timer += Time.deltaTime;
        if (timer > 0.5f)
        {
            CancelInvoke();
            isTicking = false;
            timer = 0f;
        }

        
        Debug.Log(lives);
    }

    void FixedUpdate()
    {
        if (Input.GetKey("a"))
        {
            if(offset.x >= -3 && offset.x <= 3)
            {
                offset.x -= speed * Time.deltaTime;
                transform.position = offset;
            }
            else
                offset.x = -3;
        }else if (Input.GetKey("d"))
        {
            if (offset.x >= -3 && offset.x <= 3)
            {
                offset.x += speed * Time.deltaTime;
                transform.position = offset;
            }
            else
                offset.x = 3;
            
        }



        /* //Ray cast with mouse
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, Mathf.Infinity);
        // If it hits ground, not another objects (like walls)
        if (hit.transform.tag == "Enemy")
            Debug.Log("You hit an enemy!");
       */

    }

    //collision not working.....
    
    private void OnCollisionEnter(Collision collisionInfo)
    {
        Debug.Log("test");
        if (collisionInfo.gameObject.tag == "Enemy")
        {
            Debug.Log("You hit an enemy!");
        }
    }

    void Blink()
    {
        if (!isDead)
        {
            isHit = false;
            if(player.activeSelf)
                player.SetActive(false);
            else
                player.SetActive(true);
        }
        
        
    }    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("You hit an enemy!");
            isTicking = true;
            isHit = true;
            lives--;
            InvokeRepeating("Blink", 0, blinkingSpeed);
            player.SetActive(true);
            smoochHurt.Play(0);
        }

        if (other.gameObject.tag == "End")
        {
            Debug.Log("we are in the endgame");
            platformMov.enabled = false;
            GameManager.patientRescued++;
            GameManager.paused = false;
            GameManager.mainCam.enabled = true;
            gm.ResumeMainGame(3);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isHit = false;
    }

    void checkLives()
    {
        switch (lives)
        {
            case 1:
                livesIcon.transform.GetChild(2).gameObject.SetActive(false);
                livesIcon.transform.GetChild(1).gameObject.SetActive(false);
                break;
            case 2:
                livesIcon.transform.GetChild(2).gameObject.SetActive(false);
                break;
        }
        if(lives <= 0)
        {
            Debug.Log("You lost!");
            isDead = true;
            livesIcon.transform.GetChild(2).gameObject.SetActive(false);
            livesIcon.transform.GetChild(1).gameObject.SetActive(false);
            livesIcon.transform.GetChild(0).gameObject.SetActive(false);
            platformMov.enabled = false;
            player.SetActive(false);
            livesText.GetComponent<Text>().text = "LIVES : 0";
            //you lost....
        }
    }




    // Mouse movement
    /*
	void FixedUpdate () {
        if (Input.GetAxis("Mouse X") < 0)
        {
            //Code for action on mouse moving left
            print("Mouse moved left");
            offset.x -= speed * Time.deltaTime;
            transform.position = offset;
        }
        if (Input.GetAxis("Mouse X") > 0)
        {
            //Code for action on mouse moving right
            print("Mouse moved right");
            offset.x += speed * Time.deltaTime;
            transform.position = offset;
        }
    }
    */

}
