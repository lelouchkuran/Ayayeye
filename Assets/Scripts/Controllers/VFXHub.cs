using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VFXHub : Singleton<VFXHub> {
    public GameObject tunnel_swipe;
    public GameObject right_feedback;
    public GameObject wrong_feedback;
    private Player player;
    private List<GameObject> vfx_list;

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<Player>().GetComponent<Player>();
        vfx_list = new List<GameObject>();
    }
    // Update is called once per frame
    void FixedUpdate () {
        int vfx_count;
        vfx_count = vfx_list.Count;
        if (vfx_count > 0) {
            for (int i = 0; i < vfx_count; ++i) {
                GameObject explosion = vfx_list[i];
                if (explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("End")) {
                    vfx_list.Remove(explosion);
                    --vfx_count;
                    Destroy(explosion);
                }
            }
        }
    }

    public void TunnelSwipe () {
        GameObject current = player.GeneratorNow();
        GameObject swipe = Instantiate(tunnel_swipe);
        swipe.transform.parent = current.transform;
        swipe.transform.localPosition = Vector3.zero;
        swipe.transform.localRotation = Quaternion.identity;
        swipe.transform.localScale = new Vector3(1, 1, 1);
    }

    public void PlayRight(Transform trans) {
        Play(trans, right_feedback);
    }
    
    public void PlayWrong(Transform trans) {
        Play(trans, wrong_feedback);
    }

    void Play(Transform trans, GameObject feedback) {
        Vector3 pos = trans.position;
        Vector3 dir = pos.normalized;
        pos = pos - dir;
        GameObject instance = (GameObject)Instantiate(feedback, pos, Quaternion.identity);
        instance.transform.LookAt(Vector3.zero);
        vfx_list.Add(instance);
    }
}
