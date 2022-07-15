using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeScript : MonoBehaviour
{
    public GameObject boss;
    public bool check;
    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        check = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveBridge();
    }

    public void MoveBridge()
    {
        if (check && gameObject.transform.position.y + 900 < -0.1)
        {
            gameObject.GetComponent<Rigidbody>().velocity = transform.up * Time.deltaTime * 200;
        }
    }
}
