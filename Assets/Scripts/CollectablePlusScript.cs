using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablePlusScript : MonoBehaviour
{
    private GameObject playerHUD;

    public AudioClip triggerSound;
    public float volume;
    new AudioSource audio;

    public float degreesPerSecond = 100.0f;
    public float amplitude = 0.3f;
    public float frequency = 1f;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    void Start()
    {
        audio = GetComponent<AudioSource>();
        playerHUD = GameObject.FindGameObjectWithTag("HUD");

        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }

    void Update()
    {
        // Spin object around Y-Axis
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            // Play the audio once
            audio.PlayOneShot(triggerSound, volume);

            // Disable the collectable collider and shrink it to hide
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.transform.localScale = new Vector3(0, 0, 0);

            // Increase player score
            GameManagerScript.Instance.Score += 5;
            playerHUD.GetComponent<HUDController>().UpdateScore();

            // Destroy the collectable after 1 second to allow sound to play
            Destroy(gameObject, 1f);
        }
    }
}
