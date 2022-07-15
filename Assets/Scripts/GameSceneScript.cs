using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneScript : MonoBehaviour
{
    private GameObject thePlayer;

    void Awake()
    {
        thePlayer = Instantiate((GameObject)Resources.Load("Prefabs/Player"));
    }
}
