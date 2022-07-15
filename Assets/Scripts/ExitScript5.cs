using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript5 : MonoBehaviour
{
    public ParticleSystem particles0;

    public GameObject continueMenuUI;

    void Start()
    {

    }

    void Update()
    {
        if (GameManagerScript.Instance.RoundComplete)
        {
            if (!particles0.isPlaying)
            {
                particles0.Play();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && GameManagerScript.Instance.RoundComplete)
        {
            RoundComplete();
        }
    }

    private void RoundComplete()
    {
        GameManagerScript.Instance.Round += 1;
        GameManagerScript.Instance.Level = 1;
        GameManagerScript.Instance.RoundComplete = false;
        SceneManager.LoadScene("GameScene");
    }
}
