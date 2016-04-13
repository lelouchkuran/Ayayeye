using UnityEngine;
using System.Collections;

public class CubeArrow : CubeBehavior {

	// Use this for initialization
	void Start () {
		int cover_shape = GameObject.Find ("Level").GetComponent<Level> ().CoverInfo ();
		// 0 no voer, 1 all cover, 2 line cover -, 3 line cover |
		if (cover_shape == 2 && (base.dir == 1 || base.dir == 4)) {
			cover_shape = 3;
		}
		if (cover_shape > 0) {
			// generate cover in cover_shape
		}
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}
}
