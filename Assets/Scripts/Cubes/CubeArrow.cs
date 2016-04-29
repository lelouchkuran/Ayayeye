using UnityEngine;
using System.Collections;

public class CubeArrow : CubeBehavior {
    public GameObject cover_prefab;

    // Use this for initialization
    void Start () {
		if (GameObject.Find("Level").GetComponent<LevelBase>().IsCover()) {
            // generate cover in cover_shape
            GameObject cover_ins = Instantiate(cover_prefab);
            cover_ins.transform.parent = transform;
            cover_ins.transform.localPosition = new Vector3(0, 0, Constant.Instance.CoverOffset);
            cover_ins.transform.localRotation = Quaternion.identity;
            BlockBehavior cover = cover_ins.GetComponent<BlockBehavior>() ?? null;
           // if (cover) {
                //cover.OnBirth(1, 1, Random.Range(1, 5));
            //}
        }
    }

    // Update is called once per frame
    new void Update () {
        base.Update();
    }
}
