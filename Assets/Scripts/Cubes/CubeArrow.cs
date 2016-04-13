using UnityEngine;
using System.Collections;

public class CubeArrow : CubeBehavior {
    public GameObject cover_prefab;

    // Use this for initialization
    void Start () {
        int cover_shape = GameObject.Find("Level").GetComponent<Level>().CoverInfo();
        // 0 no voer, 1 all cover, 2 line cover -, 3 line cover |
        if (cover_shape == 2 && (base.dir == 1 || base.dir == 4)) {
            cover_shape = 3;
        }

        if (cover_shape > 0) {
            // generate cover in cover_shape
            GameObject cover_ins = Instantiate(cover_prefab);
            cover_ins.transform.parent = transform;
            cover_ins.transform.localPosition = new Vector3(0, 0);
            cover_ins.transform.localRotation = Quaternion.identity;
            BlockBehavior cover = cover_ins.GetComponent<BlockBehavior>() ?? null;
            if (cover) {
                switch (cover_shape) {
                    case 1:
                        cover.OnBirth(3, 3, 2);
                        break;
                    case 2:
                        cover.OnBirth(2, 3, 2);
                        break;
                    case 3:
                        cover.OnBirth(3, 2, 2);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    // Update is called once per frame
    new void Update () {
        base.Update();
    }
}
