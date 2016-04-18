using UnityEngine;
using System.Collections;

public class DigitController : MonoBehaviour {
	public float offset;
	public float switch_time;

	GameObject digit_now;
	GameObject newdigit;
	bool exist = false;
	Color c;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeTo(GameObject newobj, Color color, bool dir = true) {
		c = color;
		c.a = 0;
		if (transform.childCount > 0) {
			digit_now = transform.GetChild (0).gameObject;
//			c = digit_now.GetComponent<Renderer> ().material.color;
		} else {
			digit_now = null;
		}

		if (newobj != null) {
			newdigit = Instantiate (newobj);
			newdigit.transform.parent = transform;
			newdigit.transform.localPosition = new Vector3 (0, offset, 0);
			newdigit.transform.localRotation = Quaternion.Euler (Vector3.zero);
			newdigit.transform.localScale = Vector3.one;

//			c = newdigit.GetComponent<Renderer> ().material.color;
		} else {
			newdigit = null;
		}
		StartCoroutine(coChangeTo(dir));
	}

	IEnumerator coChangeTo(bool dir) {
		int direction = dir ? 1 : -1;
		for (float rest_time = switch_time; rest_time > 0; rest_time -= 0.02f) {
			float ratio = 1 - rest_time / switch_time;
			if (digit_now != null) {
				digit_now.transform.localPosition = new Vector3(0, (0 - ratio) * offset * direction, 0);
				c.a = 1 - ratio;
				digit_now.GetComponent<Renderer>().material.color = c;
			}
			if (newdigit != null) {
				newdigit.transform.localPosition = new Vector3(0, (1 - ratio) * offset * direction, 0);
				c.a = ratio;
				newdigit.GetComponent<Renderer>().material.color = c;
			}
			yield return new WaitForSeconds(0.02f);
		}

		if (digit_now != null) {
			Destroy (digit_now);
		}
		if (newdigit == null) {
			Destroy(gameObject);
		}
	}
}
