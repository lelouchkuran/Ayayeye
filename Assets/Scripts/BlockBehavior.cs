using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockBehavior : MonoBehaviour {
    private GameObject[] blocks;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnBirth (int col, int row, int num_layer = 1) {
        blocks = new GameObject[row * col * num_layer];
    }

    public void OnDeath () {
        foreach(GameObject block in blocks) {
            if (block)
                Destroy(block);
        }
    }
}
