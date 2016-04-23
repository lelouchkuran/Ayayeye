using UnityEngine;
using System.Collections;

public class SelfDestroy : MonoBehaviour {
	public float tt;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		tt -= Time.deltaTime;
		if (tt < 0)
			Destroy (gameObject);
	}
}
