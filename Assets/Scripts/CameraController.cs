using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject exitText;
    public GameObject startText;
    public GameObject startText2;

    bool _isLookingToStart = false;
    bool _isLookingToStart2 = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        _isLookingToStart = false;
        _isLookingToStart2 = false;

        // Second Mode
        Vector3 startVector2 = Camera.main.WorldToViewportPoint(startText2.transform.position);
        if (startVector2.x > 0 &&
            startVector2.y > 0 &&
            startVector2.z > 0)
        {
            _isLookingToStart2 = true;
            startCountDown(1);
        }
        else
        {
            GameObject.Find("Countdown2").GetComponent<Animator>().SetTrigger("stopAnimation");
        }

        // First mode
        Vector3 startVector = Camera.main.WorldToViewportPoint(startText.transform.position);
        if (startVector.x > 0 &&
            startVector.y > 0 &&
            startVector.z > 0 &&
            !_isLookingToStart)
        {
            _isLookingToStart = true;
            startCountDown(0);
        }
        else
        {
            GameObject.Find("123").GetComponent<Animator>().SetTrigger("stopAnimation");
        }
    }

    void startCountDown (int type)
    {
        switch (type)
        {
            case 0:
                GameObject.Find("123").GetComponent<Animator>().SetTrigger("startAnimation");
                break;
            case 1:
                GameObject.Find("Countdown2").GetComponent<Animator>().SetTrigger("startAnimation");
                break;
        }
    }
}
