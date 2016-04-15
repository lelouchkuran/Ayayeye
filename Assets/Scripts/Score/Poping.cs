using UnityEngine;
using System.Collections;

public class Poping : MonoBehaviour {
	
	Color c;
	// Use this for initialization
	void Start () {
		c = transform.GetChild(0).GetComponent<Renderer>().material.color;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Pop() {
		StartCoroutine (coPop ());
	}

	IEnumerator coPop() {
		Vector3 dir = transform.position.normalized * Constant.Instance.dis_mov;
		Vector3 goa = transform.position - dir;

		for (float rest_time = 0; rest_time < Constant.Instance.time_come; rest_time += 0.02f) {
			transform.position = goa + ( 1 - rest_time / Constant.Instance.time_come) * dir;
			transform.localScale = Vector3.one * rest_time / Constant.Instance.time_come * Constant.Instance.scale;
			yield return new WaitForSeconds(0.02f);
		}
		transform.position = goa;
		transform.localScale = Vector3.one * Constant.Instance.scale;

		c.a = 1;
		for (float rest_time = 0; rest_time < Constant.Instance.time_fade; rest_time += 0.02f) {
			c.a = 1 - rest_time / Constant.Instance.time_fade;
			foreach (Transform son in transform) {
				son.gameObject.GetComponent<Renderer>().material.color = c;
			}
			yield return new WaitForSeconds(0.02f);
		}
		c.a = 0;
		foreach (Transform son in transform) {
			son.gameObject.GetComponent<Renderer>().material.color = c;
		}

		Destroy (gameObject);
	}
}
