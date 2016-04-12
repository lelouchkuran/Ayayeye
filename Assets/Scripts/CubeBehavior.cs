using UnityEngine;
using System.Collections;

public class CubeBehavior : MonoBehaviour {
	float speed = 0;
	bool flag_die = false;
	// Use this for initialization
	void Start () {
		speed = GameObject.Find ("Level").GetComponent<Level> ().GetSpeed ();
	}
	
	// Update is called once per frame
	void Update () {
		if (flag_die) return ;

		transform.localPosition -= Vector3.back * speed * Time.deltaTime;
		if (transform.localPosition.z < Constant.Instance.CubeMissDis) {
			flag_die = true;
			GameObject.Find("player").GetComponent<Player>().Miss();
		}
	}
}
