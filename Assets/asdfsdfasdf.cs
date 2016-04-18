using UnityEngine;
using System.Collections;

public class asdfsdfasdf : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Transform target = FindObjectOfType<Cardboard>().transform;
        transform.LookAt(target);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
