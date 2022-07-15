using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    private new Camera camera;
    
    public ParticleSystem muzzleFlash;
    public ParticleSystem zapperFlash;

    public AudioSource shootSound;
    public AudioSource zapperSound;

    public GameObject impact;
    public LayerMask enemyBullet;

    public float rateShot = 5f;
    public float nextShot = 0f;
    public float nextZap = 0f;
    public float force = 80f;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManagerScript.Instance.Paused && GameManagerScript.Instance.Alive)
        {
            if (Input.GetMouseButtonDown(0) && Time.time >= nextShot)
            {
                nextShot = Time.time + 1f / rateShot;
                muzzleFlash.Play();
                shootSound.Play();

                RaycastHit hit;
                Vector3 point = new Vector3(Screen.width / 2, Screen.height / 2, 0);
                Ray ray = camera.ScreenPointToRay(point);
                Debug.DrawRay(ray.origin, ray.direction * 30, Color.red);

                if (Physics.Raycast(ray, out hit, 2000f, ~enemyBullet))
                {
                    GameObject hitObject = hit.transform.gameObject;

                    if (hitObject != null && hitObject.CompareTag("Play"))
                    {
                        hitObject.GetComponent<MenuScript>().menuPlay();
                    }

                    if (hitObject != null && hitObject.CompareTag("Exit"))
                    {
                        hitObject.GetComponent<MenuScript>().menuExit();
                    }

                    if (hitObject != null && hitObject.CompareTag("TestStart"))
                    {
                        hitObject.GetComponent<TestStartScript>().testStart();
                    }
                    if (hitObject != null && hitObject.CompareTag("TestReset"))
                    {
                        hitObject.GetComponent<TestStartScript>().testReset();
                    }
                    if (hitObject != null && hitObject.CompareTag("Enemy"))
                    {
                        hitObject.GetComponent<EnemyController>().WhenHit();
                    }
                    if (hitObject != null && hitObject.CompareTag("Boss"))
                    {
                        hitObject.GetComponentInParent<BossScript>().WhenHit();
                    }

                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(-hit.normal * force);
                    }

                    GameObject hitImpact = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(hitImpact, 2f);
                }
            }

            if (Input.GetMouseButtonDown(1) && Time.time >= nextZap)
            {
                nextZap = Time.time + 1f;
                zapperFlash.Play();
                zapperSound.Play();

                RaycastHit hit;
                Vector3 point = new Vector3(Screen.width / 2, Screen.height / 2, 0);
                Ray ray = camera.ScreenPointToRay(point);
                Debug.DrawRay(ray.origin, ray.direction * 30, Color.red);

                if (Physics.Raycast(ray, out hit, 2000f, ~enemyBullet))
                {
                    GameObject hitObject = hit.transform.gameObject;

                    if (hitObject != null && hitObject.CompareTag("Enemy"))
                    {
                        hitObject.GetComponent<EnemyController>().WhenZapped();
                    }
                }
            }
        }
    }
}
