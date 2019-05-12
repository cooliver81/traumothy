using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour {

    public GameManager gm;
    Button b;

    // Use this for initialization
    void Start () {
        b = gameObject.GetComponent<Button>();
        b.onClick.AddListener(delegate () { gm.StartMainGame(); });
    }
}
        
