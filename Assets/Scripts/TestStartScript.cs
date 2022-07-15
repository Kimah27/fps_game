using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestStartScript : MonoBehaviour
{
    public GameObject playerHUD;
    public GameObject testEnemy1;
    public GameObject testEnemy2;
    public GameObject testEnemy3;
    public GameObject testEnemy4;

    void Start()
    {
        playerHUD = GameObject.FindGameObjectWithTag("HUD");
    }

    void Update()
    {
        
    }

    public void testStart()
    {
        SceneManager.LoadScene("GameScene");
        //SpawnEnemies();
    }

    public void testReset()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
    }

    public void SpawnEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
        Instantiate(testEnemy1, new Vector3(-15f, 10f, -42f), Quaternion.identity);
        Instantiate(testEnemy2, new Vector3(-5f, 12f, -35f), Quaternion.identity);
        Instantiate(testEnemy3, new Vector3(0f, 9f, -40f), Quaternion.identity);
        Instantiate(testEnemy4, new Vector3(7f, 14f, -36f), Quaternion.identity);
        Instantiate(testEnemy1, new Vector3(-10f, 20f, -42f), Quaternion.identity);
        Instantiate(testEnemy2, new Vector3(0f, 22f, -35f), Quaternion.identity);
        Instantiate(testEnemy3, new Vector3(5f, 19f, -40f), Quaternion.identity);
        Instantiate(testEnemy4, new Vector3(12f, 24f, -36f), Quaternion.identity);

        playerHUD.GetComponent<HUDController>().NewKeySet();
    }
}