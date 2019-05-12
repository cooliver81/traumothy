using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPCInteractions : MonoBehaviour {


    public AudioSource sneeze1;
    public AudioSource sneeze2;
    public AudioSource sneeze3;

    public GameManager gm;

    private GameObject NPCTriggered;

    public GameObject dialogueBox;

    static public int numInteractions = 0;

    int spaceCount = 0;
    public bool isTriggered = false;
    private enum NPC_ID {NPC1, NPC2, NPC3};
    NPC_ID npcID;

    public string[] information = new string[3];

    bool dialogueOnScreen = false;

    LevelChanger levelchanger;

    // Use this for initialization
    void Start () {
        //SET NPC INFORMATION
        information[0] = "I remember the flashing lights... the spaceship... They did something to me! I'm not crazy!"; // war
        information[1] = "I hate kisses... so many... germs... ew! Gandma no!"; // grandma -germs
        information[2] = "Doctor help me! A clown is stalking me! I can see it wherever I go, it won't leave me alone!"; // clown
        dialogueOnScreen = false;
        dialogueBox = GameObject.Find("Box");
        dialogueBox.SetActive(false);
        levelchanger = GameObject.Find("LevelChanger").GetComponent<LevelChanger>();
    }

    // Update is called once per frame
    void Update () {
        CheckIfRescued();

        if (isTriggered)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                spaceCount++;
                showDialogue();
                Debug.Log("isTriggered");
            }
            else
                Debug.Log("isNOTTriggered");
            }
        if (dialogueOnScreen && spaceCount >= 1) {
            if (Input.GetKeyDown(KeyCode.P))
            {
                spaceCount = 0;
                numInteractions++;
                dialogueOnScreen = false;
                dialogueBox.SetActive(false);
                switch (npcID)
                {
                    case NPC_ID.NPC1:
                        Debug.Log("NPC1 triggered");
                        gm.StartWar();
                        //levelchanger.fadeToLevel(2);
                        CheckClipboard(0);
                        break;
                    case NPC_ID.NPC2:
                        Debug.Log("NPC2 triggered");
                        gm.StartGrandma();
                        //levelchanger.fadeToLevel(3);
                        CheckClipboard(1);
                        break;
                    case NPC_ID.NPC3:
                        Debug.Log("NPC3 triggered");
                        gm.StartClown();
                        //levelchanger.fadeToLevel(2);
                        CheckClipboard(2);
                        break;
                }
            }
        }
            
    }

    void showDialogue() {
        dialogueOnScreen = true;
        dialogueBox.SetActive(true);
        switch (npcID)
        {
            case NPC_ID.NPC1:
                dialogueBox.transform.GetChild(2).GetComponent<Text>().text = information[0];
                sneeze1.Play(0);
                break;
            case NPC_ID.NPC2:
                dialogueBox.transform.GetChild(2).GetComponent<Text>().text = information[1];
                sneeze2.Play(0);
                break;
            case NPC_ID.NPC3:
                dialogueBox.transform.GetChild(2).GetComponent<Text>().text = information[2];
                sneeze3.Play(0);
                break;
        }
    }

    public void CheckClipboard(int npc)
    {
        //text.GetComponent<TextMesh>().text = information[npc];
    }

    public void CheckIfRescued()
    {
        if (GameManager.isRescued)
        {
            Debug.Log(GameManager.isRescued);
            GameManager.isRescued = false;
            NPCTriggered.GetComponent<BoxCollider>().enabled = false;
            isTriggered = false;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC1"))
        {
            isTriggered = true;
            npcID = NPC_ID.NPC1;
            NPCTriggered = other.gameObject;
        }
        else if (other.CompareTag("NPC2"))
        {
            isTriggered = true;
            npcID = NPC_ID.NPC2;
            NPCTriggered = other.gameObject;
        }
        else if (other.CompareTag("NPC3"))
        {
            isTriggered = true;
            npcID = NPC_ID.NPC3;
            NPCTriggered = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC1") || other.CompareTag("NPC2") || other.CompareTag("NPC3"))
        {
            isTriggered = false;
            NPCTriggered = null;

        }
    }
    /*
    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.tag == "NPC1")
        {
            isTriggered = true;
            npcID = NPC_ID.NPC2;
            NPCTriggered = collisionInfo.gameObject.gameObject;
        }
        else if (collisionInfo.gameObject.tag == "NPC2")
        {
            isTriggered = true;
            npcID = NPC_ID.NPC1;
            NPCTriggered = collisionInfo.gameObject.gameObject;
        }
        else if (collisionInfo.collider.tag == "NPC3")
        {
            isTriggered = true;
            npcID = NPC_ID.NPC3;
            NPCTriggered = collisionInfo.gameObject.gameObject;
        }
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "NPC1" || collisionInfo.gameObject.tag == "NPC2" || collisionInfo.gameObject.tag == "NPC3")
        {
            isTriggered = false;
            NPCTriggered = null;

        }
    }
    */
}
