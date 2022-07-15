using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
   public float bulletSpeed;
    void Start()
    {
        bulletSpeed = 50f;
    }

    void Update()
    {
        gameObject.transform.position += gameObject.transform.forward * Time.deltaTime * bulletSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            GameManagerScript.Instance.Health -= 10;
            //collision.gameObject.GetComponent<PlayerMovement>().HP -= 10;
            collision.gameObject.GetComponent<PlayerMovement>().WhenHit();
        }
        if (!collision.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
    }
}
