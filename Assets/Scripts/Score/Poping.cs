using UnityEngine;
using System.Collections;

public class Poping : MonoBehaviour {
	
	Color c;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Pop() {
		StartCoroutine (coPop ());
	}

	IEnumerator coPop() {
		c = transform.GetChild(0).GetComponent<Renderer>().material.color;
		Vector3 dir = transform.position.normalized * Constant.Instance.dis_mov;
		Vector3 goa = transform.position - dir;

		for (float rest_time = 0; rest_time < Constant.Instance.time_come; rest_time += 0.01f) {
			// transform.position = goa + ( 1 - rest_time / Constant.Instance.time_come) * dir;
			transform.localScale = Vector3.one * rest_time / Constant.Instance.time_come * Constant.Instance.scale;
			yield return new WaitForSeconds(0.01f);
		}
		// transform.position = goa;
		transform.localScale = Vector3.one * Constant.Instance.scale;

		Vector3 pos = transform.position;
		GameObject goa_obj = GameObject.Find("score");
		c.a = 1;

		for (float rest_time = 0; rest_time < Constant.Instance.time_down; rest_time += 0.01f) {
			float ratio = rest_time / Constant.Instance.time_down;
			transform.position = (goa_obj.transform.position - pos) * ratio + pos;
			transform.localScale = Vector3.one * (1 + (1 - ratio) * (Constant.Instance.scale - 1));
			// transform.LookAt(Vector3.zero);
			yield return new WaitForSeconds(0.01f);
			if (ratio <= 0.5f) continue;

			c.a = 1 - (rest_time / Constant.Instance.time_down - 0.5f) / 0.5f;
			foreach (Transform son in transform) {
				son.gameObject.GetComponent<Renderer>().material.color = c;
			}

		}
		c.a = 0;
		foreach (Transform son in transform) {
			son.gameObject.GetComponent<Renderer>().material.color = c;
		}

		Destroy (gameObject);

	}
}
