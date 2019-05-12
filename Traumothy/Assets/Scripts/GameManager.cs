using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


    public static int patientRescued = 0;
    public static int patientDied = 0;
    public static bool paused = false, isRescued = false;
    LevelChanger levelchanger;

    public static Camera mainCam;
    public PlayerController playerController;
    public NPCInteractions npcInteract;


    // Use this for initialization
    void Start() {
        Debug.Log(patientRescued);
        mainCam = Camera.main;
        levelchanger = GameObject.Find("LevelChanger").GetComponent<LevelChanger>();
    }

    // Update is called once per frame
    void Update() {
        if (paused)
        {
            playerController.enabled = false;
            npcInteract.enabled = false;
        }
        else
        {
            playerController.enabled = true;
            npcInteract.enabled = true;
        }

        if (patientRescued >= 3)
        {
            //WIN GAME
            SceneManager.LoadScene(0);
        } else if ((patientDied >= 3))
        {
            //LOSE GAME
            SceneManager.LoadScene(0);
        }
    }

    public void ResumeMainGame(int id)
    {
        paused = false;
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(id));
    }

    public void StartMainGame()
    {
        SceneManager.LoadScene(1);
    }
    public void StartWar()
    {
        //SceneManager.LoadScene("War", LoadSceneMode.Additive);
        levelchanger.fadeToLevel(2);
        paused = true;
    }
    public void StartGrandma()
    {
        //SceneManager.LoadScene("Old Lady", LoadSceneMode.Additive);
        levelchanger.fadeToLevel(3);
        paused = true;

    }
    public void StartClown()
    {
        //SceneManager.LoadScene("Clown", LoadSceneMode.Additive);
        levelchanger.fadeToLevel(4);
        paused = true;

    }


}
