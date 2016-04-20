using UnityEngine;
using System.Collections;

public class RotateBottom : MonoBehaviour {
    private Vector3 v;
    private Quaternion local;
    private Quaternion q;
    private GameObject obj;
    // Use this for initialization
    void Start () {
        obj = GameObject.Find("Head");
    }
	
	// Update is called once per frame
	void Update () {
        q = GameObject.Find("Head").transform.localRotation;
        v = q.eulerAngles;
        v.x = 0;
        v.z = 0;
        q.SetEulerAngles(v.x,v.y,v.z);
        //gameObject.transform.localRotation = camera.transform.rotation;
        //gameObject.transform.eulerAngles = new Vector3(transform.eulerAngles.x, camera.transform.eulerAngles.y, transform.eulerAngles.z);
        //gameObject.transform.Rotate(0,v.y,0);
        Vector3 newRot = new Vector3(gameObject.transform.eulerAngles.x, obj.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);
        gameObject.transform.rotation = Quaternion.Euler(newRot);
    }
}