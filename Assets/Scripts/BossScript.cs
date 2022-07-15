using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    private GameObject player;
    private GameObject playerHUD;
    public GameObject torus;
    public GameObject pivot;
    public GameObject path1;
    public GameObject path2;
    public Material mat1;
    public Material mat2;
    public Material mat3;
    public Material mat4;

    public Vector3 spin;
    public GameObject bullet;
    public int HP;
    public int ID;
    public int level;
    public bool zapped;
    public bool isAlive;

    public float fireRate;
    public float shotTimer;
    public float burstTimer;
    public float spinSpeed;

    private void Awake()
	{
        mat1 = (Material)Resources.Load("Materials/EnemyNeonCyan");
        mat2 = (Material)Resources.Load("Materials/EnemyNeonGreen");
        mat3 = (Material)Resources.Load("Materials/EnemyNeonMagenta");
        mat4 = (Material)Resources.Load("Materials/EnemyNeonYellow");
    }
    void Start()
    {
        isAlive = true;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHUD = GameObject.FindGameObjectWithTag("HUD");

        ID = 10;
        HP = 250;
        fireRate = 0.15f;
        burstTimer = 0f;
        SetMaterial();

        spin = new Vector3(1f, 0.3f, 1.0f);
        spinSpeed = 120f;
    }

    // Update is called once per frame
    void Update()
    {
        shotTimer += Time.deltaTime;
        burstTimer += Time.deltaTime;
        
        if (burstTimer >= 0.5f && shotTimer >= fireRate && GameManagerScript.Instance.Health > 0 && HP > 0)
        {
            Shoot();
            shotTimer = 0;
        }
        if (burstTimer >= 1.5f)
        {
            burstTimer = 0f;
		}

        if (HP > 0)
        {
            torus.transform.RotateAround(pivot.transform.position, spin, Time.deltaTime * spinSpeed);
        }
    }
    public void Shoot()
    {
        if (level == GameManagerScript.Instance.Level)
        //if (level == player.GetComponent<PlayerMovement>().level)
        {
            Vector3 trajectory = player.transform.Find("Main Camera").transform.position - pivot.transform.position;
            Vector3 origin = pivot.transform.position;
            GameObject bulletShot = Instantiate(bullet, origin, Quaternion.LookRotation(trajectory));
            Destroy(bulletShot, 3f);
        }

    }

    public void WhenHit()
    {
        HP -= 10;
        SetMaterial();
        if (HP <= 0)
        {
            StartCoroutine(Death());
        }
    }

    private IEnumerator Death()
    {
        playerHUD.GetComponent<HUDController>().Pause = true;
        path1.GetComponent<BridgeScript>().check = true;
        path2.GetComponent<BridgeScript>().check = true;
        if (zapped && isAlive)
        {
            //player.GetComponent<PlayerMovement>().score += 50;
            GameManagerScript.Instance.Score += 100;
        }
        if (isAlive)
        {
            isAlive = false;
            //player.GetComponent<PlayerMovement>().score += 50;
            GameManagerScript.Instance.Score += 200;
        }

        GameManagerScript.Instance.RoundComplete = true;

        playerHUD.GetComponent<HUDController>().UpdateScore();
        torus.GetComponent<Rigidbody>().velocity = Vector3.zero;
        torus.GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }

    public void SetMaterial()
    {
        if (ID != 9)
        {
            int num = UnityEngine.Random.Range(1, 5);
            switch (num)
            {
                case 1:
                    torus.GetComponent<Renderer>().material = mat1;
                    break;
                case 2:
                    torus.GetComponent<Renderer>().material = mat2;
                    break;
                case 3:
                    torus.GetComponent<Renderer>().material = mat3;
                    break;
                case 4:
                    torus.GetComponent<Renderer>().material = mat4;
                    break;
            }
        }
    }
}
