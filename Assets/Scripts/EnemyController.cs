using System;
using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    private GameObject playerHUD;
    public Material mat1;
    public Material mat2;
    public Material mat3;
    public Material mat4;


    public GameObject bullet;
    public int HP;
    public int ID;
    public int level;
    public bool zapped;
    public bool isAlive;

    public float fireRate;
    public float shotTimer;

	void Awake()
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


        if (gameObject.name.Contains("Cube"))
        {
            ID = 1;
            HP = 50;
            fireRate = 1f;
        }
        else if (gameObject.name.Contains("Sphere"))
        {
            ID = 2;
            HP = 30;
            fireRate = 0.7f;
        }
        else if (gameObject.name.Contains("Cylinder"))
        {
            ID = 3;
            HP = 30;
            fireRate = 0.7f;
        }
        else if (gameObject.name.Contains("Triangle"))
        {
            ID = 4;
            HP = 20;
            fireRate = 0.4f;
        }
        else if (gameObject.name.Contains("Capsule"))
        {
            ID = 9;
            HP = 30;
            fireRate = 0.8f;
        }
        SetMaterial();
        zapped = false;
    }

    void Update()
    {
        shotTimer += Time.deltaTime;
        if (shotTimer >= fireRate && GameManagerScript.Instance.Health > 0 && HP > 0)
        {
            Shoot();
            shotTimer = 0;
		}
    }

    private IEnumerator Death()
    {
        if (zapped && isAlive)
        {
            playerHUD.GetComponent<HUDController>().CheckKeys(ID);
            //player.GetComponent<PlayerMovement>().score += 50;
            GameManagerScript.Instance.Score += 50;
        }
        if (isAlive)
        {
            isAlive = false;
            //player.GetComponent<PlayerMovement>().score += 50;
            GameManagerScript.Instance.Score += 50;
        }

        playerHUD.GetComponent<HUDController>().UpdateScore();
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }

    public void WhenHit()
    {
        HP -= 10;
        if (HP <= 0)
        {
            StartCoroutine(Death());
        }
    }

    public void WhenZapped()
    {
        Zap();
        if (zapped)
        {
            gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            fireRate /= 3f;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            fireRate *= 3f;
        }
    }

    public void Unzap()
    {
        zapped = false;
        gameObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    }

    public void Zap()
    {
        zapped = !zapped;
    }

    public void Shoot()
    {
        if(level == GameManagerScript.Instance.Level)
        //if (level == player.GetComponent<PlayerMovement>().level)
        {
            Vector3 trajectory = player.transform.Find("Main Camera").transform.position - gameObject.transform.position;
            Vector3 origin = gameObject.transform.position + (trajectory.normalized * 5);
            GameObject bulletShot = Instantiate(bullet, origin, Quaternion.LookRotation(trajectory));
            Destroy(bulletShot, 3f);
        }
        
    }

    public void SetMaterial()
    {
        if (ID != 9)
        {
            int num = UnityEngine.Random.Range(1, 5);
            switch (num)
            {
                case 1:
                    gameObject.GetComponent<Renderer>().material = mat1;
                    break;
                case 2:
                    gameObject.GetComponent<Renderer>().material = mat2;
                    break;
                case 3:
                    gameObject.GetComponent<Renderer>().material = mat3;
                    break;
                case 4:
                    gameObject.GetComponent<Renderer>().material = mat4;
                    break;
            }
        }
	}
}
