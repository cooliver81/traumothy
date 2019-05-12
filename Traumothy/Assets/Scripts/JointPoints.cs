using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointPoints : MonoBehaviour {

    public int openingDirection;
    // 1 --> Bottom
    // 2 --> Top
    // 3 --> Left
    // 4 --> Right

    public int openingNeeded;

    // Use this for initialization
    void Start () {
        switch (openingDirection)
        {
            case 1:
                openingNeeded = 2;
                break;
            case 2:
                openingNeeded = 1;
                break;
            case 3:
                openingNeeded = 4;
                break;
            case 4:
                openingNeeded = 3;
                break;
        }
    }
}
