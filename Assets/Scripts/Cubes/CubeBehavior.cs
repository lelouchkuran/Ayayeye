using UnityEngine;
using System.Collections;

public class CubeBehavior : MonoBehaviour {
	protected float speed = 0;
	protected int dir, dir_show;

	bool flag_word;
	// Use this for initialization
	void Awake () {
 		speed = GameObject.Find ("Level").GetComponent<Level> ().GetSpeed ();
	}
	
	// Update is called once per frame
	public void Update () {
		transform.localPosition -= Vector3.back * speed * Time.deltaTime;
		if (transform.localPosition.z < Constant.Instance.CubeMissDis) {
			GameObject.Find("player").GetComponent<Player>().Miss();
			Miss();
		}
	}

	public virtual void SetInfo(int _dir, int _dir_show, bool _word) {
		dir = _dir;
		dir_show = _dir_show;
		flag_word = _word;
	}

	void Miss() {
		// miss effect on cube;

		Destroy (gameObject);
	}

	public void Finish(bool win) {
		if (win) {
			// right cube effect

		} else {
			// wrong cube effect

		}
		Destroy (gameObject);
	}

	public int GetDir() {
		return dir;
	}
}
