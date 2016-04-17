using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour {
	Vector3 pos, delta;
	float rest_time = 0;
	Constant c;
	// Use this for initialization
	void Start () {
		pos = transform.position;
		c = Constant.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		rest_time -= Time.deltaTime;
		if (rest_time <= 0) {
			rest_time = 0;
			transform.position = pos;
			return ;
		}

		delta = new Vector3 (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f));
		delta = delta.normalized;
		transform.position = pos + delta * Random.Range (0.0f, c.ShakeRange);
	}

	public void shake() {
		rest_time = c.ShakeTime;
	}
}
