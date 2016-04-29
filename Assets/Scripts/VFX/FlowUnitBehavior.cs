using UnityEngine;
using System.Collections;

public class FlowUnitBehavior : MonoBehaviour {
    private const float y_mag = 20, rot_mag = 120, wiggle_mag = 5;
    private GameObject head;
    private GameObject[] units;
    private float[] ys;
    private Vector3 selfdir, dir, s_v;
    private Level level;

    void Start () {
        level = GameObject.Find("Level").GetComponent<Level>();
        head = GameObject.Find("Head");
        units = new GameObject[transform.childCount];
        ys = new float[units.Length];
        for(int i = 0; i < units.Length; ++i) {
            units[i] = transform.GetChild(i).gameObject;
            ys[i] = units[i].transform.localPosition.y;
        }
    }
	
	// Update is called once per frame
	void Update () {
        selfdir = transform.position.normalized;
        dir = Vector3.SmoothDamp(dir, head.transform.position.normalized, ref s_v, 0.3f);
        float factor = Vector3.Dot(dir, selfdir);
        float wiggle = level.currentLevel() > 1 ? level.currentLevel() * wiggle_mag * Mathf.Sin(Time.time * level.currentLevel() * Mathf.PI) : 0;
        factor = Mathf.Clamp01(factor);
        factor = Mathf.Clamp(Mathf.Pow(factor, 2), 0.3f, 1) * (1 + level.currentLevel()) / 2;
        for(int i = 0; i < units.Length; ++i) {
            Vector3 pos = units[i].transform.localPosition;
            pos.y = ys[i] * factor * (y_mag + wiggle);
            units[i].transform.localPosition = pos;
            pos = Vector3.zero;
            pos.y = ys[i] * factor * (rot_mag);
            units[i].transform.LookAt(pos);
        }
	}

}
