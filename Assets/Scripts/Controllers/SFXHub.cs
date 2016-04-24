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
    public PitchClass.Pitch reduce;

    public AudioClip[] clips;

    // Use this for initialization
    void Start () {
        _player = FindObjectOfType<Player>().GetComponent<Player>();
        reduce = PitchClass.Pitch.ConstantPitch;
    }

    // Update is called once per frame
    void Update()
    {
        if (reduce == PitchClass.Pitch.ReducePitch)
            Camera.main.gameObject.GetComponent<AudioSource>().pitch = Mathf.Lerp(1, 0.75f, Time.time * 1.2f);
        else if (reduce == PitchClass.Pitch.IncreasePitch)
            Camera.main.gameObject.GetComponent<AudioSource>().pitch = Mathf.Lerp(.75f, 1, Time.time * 1.2f);
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
}
