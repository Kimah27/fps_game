using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    private static GameManagerScript instance = null;

    private int health = 100;
    private int score = 0;
    private int round = 1;
    private int level = 1;
    private float sensitivity = 100f;
    private bool key = false;
    private bool paused = false;
    private bool alive = true;
    private bool roundComplete = false;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static GameManagerScript Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameManagerObject = new GameObject();
                gameManagerObject.name = "GameManager";

                instance = gameManagerObject.AddComponent<GameManagerScript>();
            }

            return instance;
        }
    }

    public int Health
    {
        get => health;
        set => health = value;
    }

    public int Score
    {
        get => score;
        set => score = value;
    }

    public int Round
    {
        get => round;
        set => round = value;
    }

    public int Level
    {
        get => level;
        set => level = value;
    }

    public float Sensitivity
    {
        get => sensitivity;
        set => sensitivity = value;
    }

    public bool Key
    {
        get => key;
        set => key = value;
    }

    public bool Paused
    {
        get => paused;
        set => paused = value;
    }

    public bool Alive
    {
        get => alive;
        set => alive = value;
    }

    public bool RoundComplete
    {
        get => roundComplete;
        set => roundComplete = value;
    }
}
