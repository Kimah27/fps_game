using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorRotationScript : MonoBehaviour
{
    private GameObject player;
    public GameObject pivot;
    private float timeCheck;
    private float timeMax;
    private float speed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timeCheck = 0f;
        timeMax = 3f;
        speed = 6.5f;
    }

    // Update is called once per frame
    void Update()
    {
        timeCheck += Time.deltaTime;

        //if (player.GetComponent<PlayerMovement>().level == 3 && timeCheck >= timeMax)
        if(GameManagerScript.Instance.Level == 3 && timeCheck >= timeMax)
        {
            gameObject.transform.RotateAround(pivot.transform.position, transform.up, Time.deltaTime * speed);
        }
    }
}
