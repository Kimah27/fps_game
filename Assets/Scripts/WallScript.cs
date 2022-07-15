using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    private Rigidbody rbody;

    public Vector3 yDir;

    public float targetY;
    public float moveSpeed;
    public float timeCheck;
    void Start()
    {
        rbody = gameObject.GetComponent<Rigidbody>();
        moveSpeed = 800f;

        SetTrajectory();
    }

    // Update is called once per frame
    void Update()
    {
        MoveY();
    }

    public void MoveY()
    {
        timeCheck += Time.deltaTime;

        if (timeCheck > 2)
        {
            SetTrajectory();
        }

        if (yDir == Vector3.up && gameObject.transform.position.y + 600 <= targetY)
        {
            rbody.velocity = yDir * Time.deltaTime * moveSpeed;
        }

        else if (yDir == -Vector3.up && gameObject.transform.position.y + 600 > targetY)
        {
            rbody.velocity = yDir * Time.deltaTime * moveSpeed;
        }

        else
        {
            SetTrajectory();
        }
    }

    public void SetTrajectory()
    {
        targetY = Random.Range(-11.5f, 11.5f);
        timeCheck = 0;
        if (targetY >= gameObject.transform.position.y + 600)
        {
            yDir = new Vector3(0, 1, 0);
        }
        else if (targetY < gameObject.transform.position.y + 600)
        {
            yDir = new Vector3(0, -1, 0);
        }
    }
}
