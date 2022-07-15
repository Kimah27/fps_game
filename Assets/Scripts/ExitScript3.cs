using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript3 : MonoBehaviour
{
    private GameObject playerHUD;

    private GameObject exitWalls;

    public GameObject nextLevel;
    public GameObject levelDestroy;

    public ParticleSystem particles0;
    public ParticleSystem particles1;
    public ParticleSystem particles2;
    public ParticleSystem particles3;
    public ParticleSystem particles4;
    public ParticleSystem particles5;
    public ParticleSystem particles6;

    public AudioClip triggerSound;
    public float volume;
    new AudioSource audio;

    void Start()
    {
        playerHUD = GameObject.FindGameObjectWithTag("HUD");
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerScript.Instance.Key && GameManagerScript.Instance.Level == 3)
        {
            if (!particles0.isPlaying)
            {
                particles0.Play();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && GameManagerScript.Instance.Key)
        {
            exitWalls = Instantiate((GameObject)Resources.Load("Prefabs/ExitWalls3"));
            exitWalls.transform.SetParent(nextLevel.transform);
            StartCoroutine(Explosion());
        }
    }

    private IEnumerator Explosion()
    {
        playerHUD.GetComponent<HUDController>().Pause = true;
        audio.PlayOneShot(triggerSound, volume);
        yield return new WaitForSeconds(2f);
        Destroy(levelDestroy);
        particles1.Play();
        particles2.Play();
        particles3.Play();
        particles4.Play();
        particles5.Play();
        particles6.Play();
        playerHUD.GetComponent<HUDController>().NewKeySet();
        GameManagerScript.Instance.Level += 1;
        playerHUD.GetComponent<HUDController>().IncreaseTimer(120);
    }
}
