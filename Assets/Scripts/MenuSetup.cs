using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSetup : MonoBehaviour
{
    void Awake()
    {
        GameManagerScript.Instance.Alive = true;
        GameManagerScript.Instance.Health = 100;
        GameManagerScript.Instance.Score = 0;
        GameManagerScript.Instance.Round = 1;
        GameManagerScript.Instance.Level = 1;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
