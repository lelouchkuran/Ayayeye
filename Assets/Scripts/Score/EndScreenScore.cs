using UnityEngine;
using System.Collections;

public class EndScreenScore : MonoBehaviour {
    public GameObject[] digits;
    public Color c;

    // Use this for initialization
    void Start () {

        int num = GameObject.Find("ScoreController").GetComponent<ScoreController>().totalScore;

        GameObject new_holder = new GameObject("score");
        new_holder.transform.position = Vector3.zero;
        new_holder.transform.LookAt(Vector3.zero);
        new_holder.transform.localScale = Vector3.zero;

        int l = num.ToString().Length;
        float pos_x = -l * 0.5f * 0.25f;
        for (; num > 0; num /= 10, pos_x += 0.25f)
        {
            int digit = num % 10;
            GameObject newdigit = Instantiate(digits[digit]);
            newdigit.GetComponent<Renderer>().material.color = c;
            newdigit.transform.parent = new_holder.transform;
            newdigit.transform.localPosition = new Vector3(pos_x, 0, 0);
            newdigit.transform.localScale = Vector3.one;
            newdigit.transform.LookAt(Vector3.zero);
        }

        new_holder.transform.parent = transform;
        new_holder.transform.position = new Vector3(0, 0, 25f);
        new_holder.transform.localScale = Vector3.one * 20;
        new_holder.transform.Rotate(0, 180, 0);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
