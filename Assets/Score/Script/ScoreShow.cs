using UnityEngine;
using System.Collections;

public class ScoreShow: MonoBehaviour {
	public float t = 0.2f;
	// initial score, can not be negative
	public int startScore;
	// color of the score
	public Color ScoreColor;
	// the minimum time gap between two change
	public float timeGap;
	// digit prefabs
	public GameObject[] digitPrefabs;
	// horizontal distence between two numbers
	public float offsetHorizontal;

	private GameObject[] _digits;
	private int _score;
	private float _waiting = 0;
	private int _waiting_score = 0;
	// Use this for initialization
	void Start () {
		_digits = new GameObject[11];
		if (startScore < 0) {
			Debug.Log("score shold be greater or equal to 0!!");
			startScore = 0;
		}

		// clean
		while (transform.childCount > 0) {
			Destroy(transform.GetChild(0).gameObject);
		}

		// the start num
		if (startScore == 0) {
			_digits[0] = Generate(0);
			_digits[0].GetComponent<DigitController>().ChangeTo(digitPrefabs[0], ScoreColor);
		}
		for (int i = 0, num = startScore; num > 0; num /= 10, i++) {
			_digits[i] = Generate(i);
			_digits[i].GetComponent<DigitController>().ChangeTo(digitPrefabs[num % 10], ScoreColor);
		}
		_score = startScore;
	}

	public void Add(int delta) {
		// not too fast
		if (_waiting > 0) {
			_waiting_score += delta;
			return;
		} else {
			delta += _waiting_score;
			_waiting_score = 0;
		}
		if (delta == 0) {
			return;
		}

		// change
		int new_score = _score + delta;
		if (new_score < 0) {
			return ;
		}
		Change (new_score);
	}

	public void Change(int new_score) {
		if (_waiting > 0)
			return;
		if (new_score < 0) {
			new_score = 0;
		}
		// dir to move
		bool dir = new_score - _score < 0;
		
		for (int i = 0, num = _score, new_num = new_score; num > 0 || new_num > 0; num /= 10, new_num /= 10, i++) {
			if (num == 0 && i!=0) {
				_digits[i] = Generate(i);
				_digits[i].GetComponent<DigitController>().ChangeTo(digitPrefabs[new_num % 10], ScoreColor, dir);
				continue;
			}
			if (new_num == 0) {
				if (i != 0) _digits[i].GetComponent<DigitController>().ChangeTo(null, ScoreColor, dir);
				else _digits[i].GetComponent<DigitController>().ChangeTo(digitPrefabs[new_num % 10], ScoreColor, dir);
				continue;
			}
			
			if (num % 10 != new_num % 10) {
				_digits[i].GetComponent<DigitController>().ChangeTo(digitPrefabs[new_num % 10], ScoreColor, dir);
				continue;
			}
		}
		_score = new_score;
		_waiting = timeGap;
	}

	// holder
	GameObject Generate(int p) {
		GameObject holder = new GameObject("numHolder");
		holder.AddComponent<DigitController> ();
		holder.GetComponent<DigitController> ().offset = 0.25f;
		holder.GetComponent<DigitController> ().switch_time = t;
			//Instantiate(numHolder);
		holder.transform.parent = transform;
		holder.transform.localPosition = new Vector3(0 - offsetHorizontal * p, 0, 0);
		holder.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
		holder.transform.localScale = Vector3.one;
		return holder;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha0)) {
			Add(12);
		}
		if (Input.GetKeyDown (KeyCode.Alpha9)) {
			Add(-6);
		}
		if (Input.GetKeyDown (KeyCode.Alpha8)) {
			Change (100);
		}

		_waiting -= Time.deltaTime;
	}

}