using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    public GameObject cameraHead;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //transform.RotateAround(Vector3.zero, Vector3.up, yVal * Time.deltaTime);

        //Vector3 eulerRotation = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);

        //transform.rotation = Quaternion.Euler(eulerRotation);
    }
}
