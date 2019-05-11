using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRestart : MonoBehaviour {

    public GameObject player;
    public WarPlayerController playerControl;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.position = player.transform.position;
        playerControl.enabled = true;
    }
}
