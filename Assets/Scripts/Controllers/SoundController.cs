using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

    public AudioClip[] audioSources;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void playCountdown (int index)
    {
        AudioSource objectSource = GetComponent<AudioSource>();
        switch (index)
        {
            case 0:
                {
                    objectSource.clip = audioSources[0];
                    objectSource.Play();
                    break;
                }
            case 1:
                {
                    objectSource.clip = audioSources[1];
                    objectSource.Play();
                    break;
                }
            case 2:
                {
                    objectSource.clip = audioSources[2];
                    objectSource.Play();
                    break;
                }
        }
    }
}
