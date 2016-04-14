using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    public GameObject cameraHead;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float yVal = cameraHead.transform.rotation.y;
        transform.rotation = new Quaternion(transform.rotation.x, yVal, transform.rotation.z, 0.0f);

	}
}
