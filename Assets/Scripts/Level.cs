using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

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

}
