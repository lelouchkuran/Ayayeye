using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject exitText;
    public GameObject startText;

    bool _isLookingToStart = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        _isLookingToStart = false;

        Vector3 exitVector = Camera.main.WorldToViewportPoint(exitText.transform.position);
        if (exitVector.x > 0 &&
            exitVector.y > 0 &&
            exitVector.z > 0)
        {
            Application.Quit();
        }

        Vector3 startVector = Camera.main.WorldToViewportPoint(startText.transform.position);
        if (startVector.x > 0 &&
            startVector.y > 0 &&
            startVector.z > 0 &&
            !_isLookingToStart)
        {
            _isLookingToStart = true;
            startCountDown();
        }
        else
        {
            GameObject.Find("123").GetComponent<Animator>().SetTrigger("stopAnimation");
        }
    }

    void startCountDown ()
    {
        GameObject.Find("123").GetComponent<Animator>().SetTrigger("startAnimation");
    }
}
