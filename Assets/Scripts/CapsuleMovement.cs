using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleMovement : MonoBehaviour
{
    private Rigidbody rbody;

    public int rand;

    public Vector3 moveDir;
    public Vector3 deflate;

    public float maxY;
    public float minY;
    public float spinSpeed;
    public float moveSpeed;
    public float timeCheck;
    public float timeMax;
    public float distWall;
    public float distFloor;
    public LayerMask layerWall;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        moveDir = SetDirection();
        transform.rotation = Quaternion.LookRotation(moveDir);
        moveSpeed = 10f;
        distWall = 20f;
        distFloor = 30f;
        timeMax = 2f;
        deflate = new Vector3(-0.75f, -0.75f, -0.75f);
    }

    
    void Update()
    {
        rbody.velocity = moveDir * moveSpeed;
        timeCheck += Time.deltaTime;

        if (Physics.Raycast(transform.position, transform.forward, distWall, layerWall))
        {
            moveDir = SetDirection();
            transform.rotation = Quaternion.LookRotation(moveDir);
            timeCheck = 0f;
		}

        if (!Physics.Raycast(transform.position, transform.forward - (transform.up / 2), distFloor, layerWall))
        {
            moveDir = SetDirection();
            transform.rotation = Quaternion.LookRotation(moveDir);
            timeCheck = 0f;
        }

        if (timeCheck >= timeMax)
        {
            moveDir = SetDirection();
            transform.rotation = Quaternion.LookRotation(moveDir);
            timeCheck = 0f;
        }

        if (!gameObject.GetComponent<EnemyController>().isAlive)
        {
            gameObject.transform.localScale += deflate * Time.deltaTime;
		}
    }

    public Vector3 SetDirection()
    {
        Vector3 temp = new Vector3();
        int num = Random.Range(0, 4);

        if (num == 0)
        {
            temp = transform.forward;
        }
        else if (num == 1)
        {
            temp = transform.right;
        }
        else if (num == 2)
        {
            temp = -transform.forward;
        }
        else if (num == 3)
        {
            temp = -transform.right;
        }

        return temp;
    }
}
