using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


    public static int patientRescued = 0;
    public static bool paused = false;

    public static Camera mainCam;
    public PlayerController playerController;
    public NPCInteractions npcInteract;
    // Use this for initialization
    void Start() {
        Debug.Log(patientRescued);
        mainCam = Camera.main;
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
        SceneManager.LoadScene("War", LoadSceneMode.Additive);
        paused = true;
    }
    public void StartGrandma()
    {
        SceneManager.LoadScene("Old Lady", LoadSceneMode.Additive);
        paused = true;

    }
    public void StartClown()
    {
        SceneManager.LoadScene("Clown", LoadSceneMode.Additive);
        paused = true;

    }
}
