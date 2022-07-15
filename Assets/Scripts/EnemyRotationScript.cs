using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class EnemyRotationScript : MonoBehaviour
{
    private Rigidbody rbody;

    public int rand;

    public Vector2 startPos;
    public Vector3 spin;
    public Vector3 moveDir;
    public Vector3 yDir;

    public float maxY;
    public float minY;
    public float spinSpeed;
    public float moveSpeed;
    public float timeCheck;

    void Start()
    {
        rbody = gameObject.GetComponent<Rigidbody>();
        maxY = gameObject.transform.position.y + Random.Range(2, 10);
        minY = gameObject.transform.position.y - Random.Range(2, 10);

        spin = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
        spinSpeed = 1.5f;
        moveSpeed = 4f;

        rand = Random.Range(0,2);
        if (rand == 0)
        {
            yDir = new Vector3(0,1,0);
		}
        else if (rand == 1)
        {
            yDir = new Vector3(0, -1, 0);
        }
    }


    void Update()
    {
        if (gameObject.GetComponent<EnemyController>().level != 3)
        {
            if (gameObject.transform.position.y >= maxY && Time.time > timeCheck)
            {
                timeCheck = Time.time + 2f;
                yDir = -yDir;
            }
            if (gameObject.transform.position.y <= minY && Time.time > timeCheck)
            {
                timeCheck = Time.time + 2f;
                yDir = -yDir;
            }
            if (gameObject.GetComponent<EnemyController>().HP > 0)
            {
                rbody.velocity = yDir * moveSpeed;
            }
        }
        
        if (gameObject.GetComponent<EnemyController>().HP > 0)
        {
            transform.Rotate(spin * spinSpeed);
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
