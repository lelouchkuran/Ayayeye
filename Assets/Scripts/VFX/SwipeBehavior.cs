using UnityEngine;
using System.Collections;

public class SwipeBehavior : MonoBehaviour {
    Animator _anim;

    // Use this for initialization
    void Start () {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("End"))
            Destroy(transform.parent.gameObject);
    }
}
