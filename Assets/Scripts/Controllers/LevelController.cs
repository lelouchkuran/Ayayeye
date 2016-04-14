using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {

    public KeyCode[] buttons;
    public GameObject explosion;

    public GameObject[] boxes;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        //{
        //    if (Input.GetKeyDown(kcode))
        //        Debug.Log("KeyCode down: " + kcode);
        //}

        if (Input.GetKeyDown(buttons[1]))
        {
            Debug.Log("O");

            GameObject box = boxes[0] as GameObject;
            Instantiate(explosion);
            explosion.transform.position = box.transform.position;
            explosion.GetComponent<ParticleSystem>().Play();
            Destroy(box);
            Invoke("newLevel", explosion.GetComponent<ParticleSystem>().duration);
        }
        else if (Input.GetKeyDown(buttons[0]))
        {
            Debug.Log("X");

            GameObject box = boxes[1] as GameObject;
            Instantiate(explosion);
            explosion.transform.position = box.transform.position;
            explosion.GetComponent<ParticleSystem>().Play();
            Destroy(box);
            Application.Quit();
        }
    }

    void newLevel ()
    {
        Application.LoadLevel("Main");
    }
}
