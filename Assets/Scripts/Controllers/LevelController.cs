using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {

    public KeyCode[] buttons;
    public GameObject explosion;
    public GameObject startText;
    public GameObject startText2;
    public GameObject[] boxes;
    float _speed = 20;
	// Use this for initialization
	void Start () {
        startText = GameObject.Find("Start");
        startText2 = GameObject.Find("Start2");
    }

    // Update is called once per frame
    void Update () {
        foreach (GameObject box in boxes)
        {
            box.transform.Rotate(new Vector3(_speed * Time.deltaTime, _speed * Time.deltaTime, _speed * Time.deltaTime), Space.World);
        }

        //foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        //{
        //    if (Input.GetKeyDown(kcode))
        //        Debug.Log("KeyCode down: " + kcode);
        //}

        if (Input.GetKeyDown(buttons[0]))
        {
            Debug.Log("O");

            if (Application.loadedLevel == 2)
            {
                Application.LoadLevel("Main");
            }
            else
            {
                GameObject box = boxes[0] as GameObject;
                Instantiate(explosion);
                explosion.transform.position = box.transform.position;
                explosion.GetComponent<ParticleSystem>().Play();
                Destroy(box);
                Invoke("newLevel", explosion.GetComponent<ParticleSystem>().duration);
            }
        }
        else if (Input.GetKeyDown(buttons[1]))
        {
            Debug.Log("X");
            if (Application.loadedLevel == 2)
            {
                Application.Quit();
            }
            else
            {
                GameObject box = boxes[1] as GameObject;
                Instantiate(explosion);
                explosion.transform.position = box.transform.position;
                explosion.GetComponent<ParticleSystem>().Play();
                Destroy(box);
                Application.Quit();
            }
        }
    }

    public void newLevel (int level)
    {
        switch (level)
       { 
            case 0:
                Application.LoadLevel("Main");
                break;
            case 1:
                Application.LoadLevel("TimeMode");
                break;
        }
    }

    public void playFirstSound ()
    {
        Vector3 startVector = Camera.main.WorldToViewportPoint(startText.transform.position);
        Vector3 startVector2 = Camera.main.WorldToViewportPoint(startText2.transform.position);

        if ((startVector.x > 0 &&
            startVector.y > 0 &&
            startVector.z > 0) ||
            (startVector2.x > 0 &&
            startVector2.y > 0 &&
            startVector2.z > 0))
        {
            GameObject.Find("SoundManager").GetComponent<SoundController>().playCountdown(0);
        }
    }

    public void playSecondSound()
    {
        Vector3 startVector = Camera.main.WorldToViewportPoint(startText.transform.position);
        Vector3 startVector2 = Camera.main.WorldToViewportPoint(startText2.transform.position);

        if ((startVector.x > 0 &&
            startVector.y > 0 &&
            startVector.z > 0) ||
            (startVector2.x > 0 &&
            startVector2.y > 0 &&
            startVector2.z > 0))
        {
            GameObject.Find("SoundManager").GetComponent<SoundController>().playCountdown(1);
        }
    }

    public void playLastSound()
    {
        Vector3 startVector = Camera.main.WorldToViewportPoint(startText.transform.position);
        Vector3 startVector2 = Camera.main.WorldToViewportPoint(startText2.transform.position);

        if ((startVector.x > 0 &&
            startVector.y > 0 &&
            startVector.z > 0) ||
            (startVector2.x > 0 &&
            startVector2.y > 0 &&
            startVector2.z > 0))
        {
            GameObject.Find("SoundManager").GetComponent<SoundController>().playCountdown(2);
        }
    }
}
