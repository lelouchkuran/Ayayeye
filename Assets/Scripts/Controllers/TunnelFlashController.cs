using UnityEngine;
using System.Collections;

public class TunnelFlashController : MonoBehaviour {
	Constant c;
	void Start() {
		c = Constant.Instance;
	}

	public void Right() {
		StopCoroutine ("CoRight");
		StopCoroutine ("CoWrong");
		StartCoroutine("CoRight");
	}

	public void Wrong() {
		StopCoroutine ("CoRight");
		StopCoroutine ("CoWrong");
		StartCoroutine("CoWrong");
	}

	IEnumerator CoRight() {
		Color delta = c.ColorRight - c.ColorNormal;
		float tt = c.FlashPeriod_Right;

		for (int i = 0; i < c.FlashTimes_Right; ++i) {
			// turn
			for (float rest_time = tt; rest_time >= 0; rest_time -= 0.01f) {
				float ratio = 1 - rest_time / tt;
				foreach (Transform trans in transform) {
					trans.gameObject.GetComponent<Renderer>().material.color = c.ColorNormal + delta * ratio;
				}
				yield return new WaitForSeconds(0.01f);
			}
			foreach (Transform trans in transform) {
				trans.gameObject.GetComponent<Renderer>().material.color =  c.ColorRight;
			}

			// stay
			yield return new WaitForSeconds(c.FlashStay_Right);

			// turn wrong
			for (float rest_time = tt; rest_time >= 0; rest_time -= 0.01f) {
				float ratio = rest_time / tt;
				foreach (Transform trans in transform) {
					trans.gameObject.GetComponent<Renderer>().material.color = c.ColorNormal + delta * ratio;
				}
				yield return new WaitForSeconds(0.01f);
			}
			foreach (Transform trans in transform) {
				trans.gameObject.GetComponent<Renderer>().material.color = c.ColorNormal;
			}
		}
	}

	IEnumerator CoWrong() {
		Color delta = c.ColorWrong - c.ColorNormal;
		float tt = c.FlashPeriod_Wrong;
		
		for (int i = 0; i < c.FlashTimes_Wrong; ++i) {
			// turn
			for (float rest_time = tt; rest_time >= 0; rest_time -= 0.01f) {
				float ratio = 1 - rest_time / tt;
				foreach (Transform trans in transform) {
					trans.gameObject.GetComponent<Renderer>().material.color = c.ColorNormal + delta * ratio;
				}
				yield return new WaitForSeconds(0.01f);
			}
			foreach (Transform trans in transform) {
				trans.gameObject.GetComponent<Renderer>().material.color = c.ColorWrong;
			}
			
			// stay
			yield return new WaitForSeconds(c.FlashStay_Right);
			
			// turn wrong
			for (float rest_time = tt; rest_time >= 0; rest_time -= 0.01f) {
				float ratio = rest_time / tt;
				foreach (Transform trans in transform) {
					trans.gameObject.GetComponent<Renderer>().material.color = c.ColorNormal + delta * ratio;
				}
				yield return new WaitForSeconds(0.01f);
			}
			foreach (Transform trans in transform) {
				trans.gameObject.GetComponent<Renderer>().material.color = c.ColorNormal;
			}
		}		
	}
}
