using UnityEngine;
using System.Collections;

public class NumGenerator : MonoBehaviour {
    public GameObject holder;
	public GameObject[] digits;
	public Color c;
	public GameObject particle;
	public ScoreShow score;

	void Start() {
		//c = Color.white;
		c.a = 1;
	}

	public void Generate(int num, Vector3 pos, GameObject father) {
		if (num <= 0) {
			Debug.Log("score less or equal to zero");
			return ;
		}
		int num0 = num;

		GameObject new_holder = Instantiate(holder);
		new_holder.transform.position = pos;
		new_holder.transform.localScale = Vector3.zero;

		int l = num.ToString().Length;
		float pos_x = -l * 0.5f * 0.25f;
		for (; num > 0; num /= 10, pos_x += 0.25f) {
			int digit = num % 10;
			GameObject newdigit = Instantiate(digits[digit]);
			newdigit.GetComponent<Renderer>().material.color = c;
			newdigit.transform.parent = new_holder.transform;
			newdigit.transform.localPosition = new Vector3(pos_x, 0, 0);
			newdigit.transform.localScale = Vector3.one;
		}
		new_holder.transform.LookAt(Vector3.zero);
		new_holder.transform.parent = father.transform;
		GameObject trail = Instantiate (particle);
		trail.transform.parent = new_holder.transform;
		trail.transform.localPosition = Vector3.zero;
		trail.transform.localScale = Vector3.one;
		new_holder.AddComponent<Poping>();
		new_holder.GetComponent<Poping> ().Set (score, num0);
		new_holder.GetComponent<Poping>().Pop();
	}

}
