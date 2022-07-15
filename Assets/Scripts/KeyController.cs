using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyController : MonoBehaviour
{
    public Sprite cube;
    public Sprite sphere;
    public Sprite cylinder;
    public Sprite triangle;
    public Sprite tick;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public int KeyGen()
    {
        int num = Random.Range(1,5);
        switch (num)
        {
            case 1:
                gameObject.GetComponent<Image>().sprite = cube;
                break;
            case 2:
                gameObject.GetComponent<Image>().sprite = sphere;
                break;
            case 3:
                gameObject.GetComponent<Image>().sprite = cylinder;
                break;
            case 4:
                gameObject.GetComponent<Image>().sprite = triangle;
                break;
        }

        return num;
    }

    public void Completed()
    {
        gameObject.GetComponent<Image>().sprite = tick;
    }
}
