using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{
    public GameObject player;
    public TMP_Text textHP;
    public TMP_Text textscore;
    public TMP_Text texttimer;
    public TMP_Text textcenter;
    public float timeRemaining;
    public float minutes;
    public float seconds;
    public string timeString;
    private GameObject k1;
    private GameObject k2;
    private GameObject k3;
    private int v1;
    private int v2;
    private int v3;
    private bool b1;
    private bool b2;
    private bool b3;
    private bool pause;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        FormatTime();
        UpdateHP();
        UpdateScore();
        k1 = GameObject.Find("Key1");
        k2 = GameObject.Find("Key2");
        k3 = GameObject.Find("Key3");
        NewKeySet();
        pause = true;
        StartTimer();
    }

    void Update()
    {
        UpdateTimer();
    }

    public void NewKeySet()
    {
        v1 = k1.GetComponent<KeyController>().KeyGen();
        v2 = k2.GetComponent<KeyController>().KeyGen();
        v3 = k3.GetComponent<KeyController>().KeyGen();
        b1 = false;
        b2 = false;
        b3 = false;
        //player.GetComponent<PlayerMovement>().key = false;
        GameManagerScript.Instance.Key = false;
    }

    public void CheckKeys(int id)
    {
        if (!b1 && v1 == id)
        {
            b1 = true;
            k1.GetComponent<KeyController>().Completed();
        }
        else if (!b2 && v2 == id)
        {
            b2 = true;
            k2.GetComponent<KeyController>().Completed();
        }
        else if (!b3 && v3 == id)
        {
            b3 = true;
            k3.GetComponent<KeyController>().Completed();
        }
        if (b1 && b2 && b3)
        {
            //player.GetComponent<PlayerMovement>().key = true;
            GameManagerScript.Instance.Key = true;
        }
	}

    public void UpdateHP()
    {
        //textHP.SetText(player.GetComponent<PlayerMovement>().HP.ToString());
        textHP.SetText(GameManagerScript.Instance.Health.ToString());
    }

    public void UpdateScore()
    {
        //textscore.SetText(player.GetComponent<PlayerMovement>().score.ToString());
        textscore.SetText(GameManagerScript.Instance.Score.ToString());
        
    }

    public void UpdateTimer()
    {
        if (!pause && timeRemaining > 0)
        {
            FormatTime();
            texttimer.SetText(minutes.ToString("00") + ":" + seconds.ToString("00"));
            timeRemaining -= Time.deltaTime;
        }
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            FormatTime();
            texttimer.SetText(minutes.ToString("00") + ":" + seconds.ToString("00"));
            StartCoroutine(player.GetComponent<PlayerMovement>().PlayerDeath());
        }
	}

    public bool Pause
    {
        get => pause;
        set => pause = value;
	}

    public void StartTimer()
    {
        timeRemaining = 180;
        texttimer.SetText(minutes.ToString() + ":" + seconds.ToString());
        pause = false;
	}

    public void IncreaseTimer(int seconds)
    {
        timeRemaining += seconds;
        pause = false;
	}

    public void FormatTime()
    {
        minutes = Mathf.Floor(timeRemaining / 60);
        seconds = Mathf.Floor(timeRemaining % 60);
	}
}
