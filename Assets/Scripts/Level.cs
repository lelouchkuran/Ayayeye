using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

    public delegate void LevelInHandler ();
    public delegate void LevelOutHandler ();
    public event LevelInHandler LevelIn;
    public event LevelOutHandler LevelOut;

    // Use this for initialization
    void Start () {

    }


	public bool IsWord() {
		return (Random.Range (0, 2) == 0);
	}

	public bool IsOppo() {
		return (Random.Range (0, 2) == 0);
	}

	public int CoverInfo() {
		// 0 no voer, 1 all cover, 2 line cover -, 3 line cover |
		return Random.Range (0, 3);
	}

    public float GetSpeed () {
        return 1.0f;
    }

    // TODO
    public void TransLevel () {
        if (LevelOut != null)
            LevelOut();
        // TODO
        if (LevelIn != null)
            LevelIn();
    }
}
