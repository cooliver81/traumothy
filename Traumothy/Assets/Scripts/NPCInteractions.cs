using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCInteractions : MonoBehaviour {

    private GameObject NPCTriggered;
    public GameManager gm;
    public GameObject text;

    public bool isTriggered = false;
    private enum NPC_ID {NPC1, NPC2, NPC3};
    NPC_ID npcID;

    public string[] information = new string[3];

    // Use this for initialization
    void Start () {
        //SET NPC INFORMATION
        information[0] = "Oliver is cool";
        information[1] = "Celinetherapper";
        information[2] = "helena stinks";
    }
	
	// Update is called once per frame
	void Update () {
        if (isTriggered)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("isTriggered");
                switch (npcID)
                {
                    case NPC_ID.NPC1:
                        Debug.Log("NPC1 triggered");
                        gm.StartWar();
                        CheckClipboard(0);
                        break;
                    case NPC_ID.NPC2:
                        Debug.Log("NPC2 triggered");
                        gm.StartGrandma();
                        CheckClipboard(1);
                        break;
                    case NPC_ID.NPC3:
                        Debug.Log("NPC3 triggered");
                        gm.StartClown();
                        CheckClipboard(2);
                        break;
                }
            }
            else
                Debug.Log("isNOTTriggered");
            }
    }

    public void CheckClipboard(int npc)
    {
        //text.GetComponent<TextMesh>().text = information[npc];
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
