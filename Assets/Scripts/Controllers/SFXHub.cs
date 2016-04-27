using UnityEngine;
using System.Collections;

public class PitchClass
{
    public enum Pitch
    {
        ReducePitch,
        IncreasePitch,
        ConstantPitch
    }
}

public class SFXHub : Singleton<SFXHub>
{
    private Player _player;
    private Level _level;
    public PitchClass.Pitch reduce;

    public AudioClip[] clips;

    // Use this for initialization
    void Start () {
        _player = FindObjectOfType<Player>().GetComponent<Player>();
        _level = FindObjectOfType<Level>().GetComponent<Level>();
        reduce = PitchClass.Pitch.ConstantPitch;
    }

    // Update is called once per frame
    void Update()
    {
        if (reduce == PitchClass.Pitch.ReducePitch)
            Camera.main.gameObject.GetComponent<AudioSource>().pitch = Mathf.Lerp(1, 0.75f, Time.time * 1.2f);
        else if (reduce == PitchClass.Pitch.IncreasePitch)
            Camera.main.gameObject.GetComponent<AudioSource>().pitch = Mathf.Lerp(.75f, 1, Time.time * 1.2f);
        if (_level.currentLevel() == 1)
        {
            Camera.main.gameObject.GetComponent<AudioSource>().clip = clips[3];
            if (!Camera.main.gameObject.GetComponent<AudioSource>().isPlaying)
                Camera.main.gameObject.GetComponent<AudioSource>().Play();
        }
        if (_level.currentLevel() == 2)
        {
            Camera.main.gameObject.GetComponent<AudioSource>().clip = clips[4];
            if (!Camera.main.gameObject.GetComponent<AudioSource>().isPlaying)
                Camera.main.gameObject.GetComponent<AudioSource>().Play();
        }
        if (_level.currentLevel() == 3)
        {
            Camera.main.gameObject.GetComponent<AudioSource>().clip = clips[5];
            if (!Camera.main.gameObject.GetComponent<AudioSource>().isPlaying)
                Camera.main.gameObject.GetComponent<AudioSource>().Play();
        }
    }

    public void changePitch (PitchClass.Pitch pitchReduction)
    {
        reduce = pitchReduction;
    }

    public void playWrongSound ()
    {
        GetComponent<AudioSource>().clip = clips[0];
        GetComponent<AudioSource>().Play();
    }

    public void playRightSound()
    {
        GetComponent<AudioSource>().clip = clips[Random.Range(1,3)];
        GetComponent<AudioSource>().Play();
    }
}
