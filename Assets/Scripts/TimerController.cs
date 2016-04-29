using UnityEngine;
using System.Collections;

public class TimerController : MonoBehaviour {
	// initial time to count down
	public int startTime;
	public int flashTime;
	// color of the time
	public Color TimeColor;
	public Color FinalColor;

	public Vector2 flashRange;
	public float flashSpeed;
	// digits' prefabs
	public GameObject[] digits;
	DigitController min, sec_t, sec_o;
	// waiting time for operate
	public float timeGap;
	public float timeChange;

	float rest_time;
	int time_next;
	int time_show;
	float _waiting;
	bool _flash_flag = false;
	int _flash_dir = 1;
	// Use this for initialization
	void Start () {
		if (digits.Length != 10) {
			Debug.Log("Digits are not set correctly");
		}
		if (startTime >= 600) {
			Debug.Log("too long time, max is 600");
			startTime = 599;
		}
		if (startTime < 1) {
			Debug.Log("too small time");
			startTime = 1;
		}
		transform.FindChild ("Sphere0").gameObject.GetComponent<Renderer> ().material.color = TimeColor;
		transform.FindChild ("Sphere1").gameObject.GetComponent<Renderer> ().material.color = TimeColor;

		min = transform.FindChild ("min").gameObject.GetComponent<DigitController>();
		sec_t = transform.FindChild ("sec_tenth").gameObject.GetComponent<DigitController>();
		sec_o = transform.FindChild ("sec_one").gameObject.GetComponent<DigitController>();
		if (timeGap < timeChange + 0.15f) {
			Debug.Log("timeGap should be greater than timeChange");
			timeGap = timeChange + 0.15f;
		}
		min.switch_time = timeChange;
		sec_t.switch_time = timeChange;
		sec_o.switch_time = timeChange;

		time_show = get_time (startTime);
		rest_time = startTime;
		time_next = startTime - 1;
		min.ChangeTo (digits [time_show / 100], TimeColor);
		sec_t.ChangeTo (digits [time_show % 100 / 10], TimeColor);
		sec_o.ChangeTo (digits [time_show % 10], TimeColor);
		_waiting = timeGap;
	}

	public float GetTime() {
		return rest_time;
	}

	public float GetTimePercent() {
		return rest_time / startTime;
	}

	public void Add(float t) {
		SetTime (rest_time + t);
	}

	public void SetTime(float t) {
		if (t <= 0) {
			TimeUp();
		}

		rest_time = (int)t;
		time_next = (int)t - 1;
		if (_waiting > 0) {
			Debug.Log(_waiting);
			return;
		}
		time_show = get_time (time_next);
		min.ChangeTo (digits [time_show / 100], TimeColor);
		sec_t.ChangeTo (digits [time_show % 100 / 10], TimeColor);
		sec_o.ChangeTo (digits [time_show % 10], TimeColor);
		_waiting = timeGap;
	}

	// Update is called once per frame
	void Update () {
		rest_time -= Time.deltaTime;
		_waiting -= Time.deltaTime;
		if (_flash_flag) {
			Flash();
		} else if (rest_time < flashTime && (!_flash_flag)) {
			_flash_flag = true;
			TimeColor = FinalColor;
			foreach (Transform son0 in transform) {
				if (son0.gameObject.GetComponent<Renderer>() != null)
					son0.gameObject.GetComponent<Renderer>().material.color = FinalColor;
				else {
					foreach (Transform son1 in son0.transform)
						son1.gameObject.GetComponent<Renderer>().material.color = FinalColor;
					min.ChangeColor(FinalColor);
					sec_t.ChangeColor(FinalColor);
					sec_o.ChangeColor(FinalColor);
				}
			}
		}
		if (_waiting > 0)
			return;

		if (rest_time <= time_next || time_show != get_time(time_next + 1)) {
			if (time_next < 0) {
				TimeUp();
				return ;
			}
			// next digit
			int time_now = get_time(time_next);
			time_next--;
			if (time_now / 100 != time_show / 100) {
				min.ChangeTo (digits [time_now / 100], TimeColor);
			}
			if (time_now % 100 / 10 != time_show % 100 / 10) {
				sec_t.ChangeTo (digits [time_now % 100 / 10], TimeColor);
			}
			if (time_now % 10 != time_show % 10) {
				sec_o.ChangeTo (digits [time_now % 10], TimeColor);
			}
			time_show = time_now;
			_waiting = timeGap;
		}
	}

	int get_time(int t) {
		int min_num = t / 60;
		int sec_t_num = t % 60 / 10;
		int sec_o_num = t % 10;
		return min_num * 100 + sec_t_num * 10 + sec_o_num;
	}

	void TimeUp() {
		Debug.Log("time up!");
		Application.LoadLevel ("End");
	}

	void Flash() {
		transform.localScale += _flash_dir * flashSpeed * Time.deltaTime * Vector3.one;
		if (transform.localScale.x > flashRange.y)
			_flash_dir = -1;
		if (transform.localScale.x < flashRange.x)
			_flash_dir = 1;
	}
}
