using UnityEngine;
using System.Collections;

public class TunnelOffsetSpeedController : MonoBehaviour {
	Constant c;
	float speed = 0;
	// Use this for initialization
	void Start () {
		c = Constant.Instance;
	}
	
	public void Right() {
		speed += c.SpeedIncForOffset;
	}
	public void Wrong() {
		speed = 0;
	}
	public float GetSpeed() {
		return speed;
	}
}
