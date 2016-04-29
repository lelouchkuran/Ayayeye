using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VFXHub : Singleton<VFXHub> {
    public GameObject tunnel_swipe;
    public GameObject right_feedback;
    public GameObject wrong_feedback;
    public GameObject flow_unit;
    public Material flow_light;
    public Color[] level_color;
    private Player player;
    private Level level;
    private List<GameObject> vfx_list;
    private float target_angular, current_angular, ref_angular;
    private bool flag_rot;

    // Use this for initialization
    void Start () {
        level = GameObject.Find("Level").GetComponent<Level>();
        player = FindObjectOfType<Player>().GetComponent<Player>();
        vfx_list = new List<GameObject>();
        FlowGenerate();
        LevelUp();
        level.LevelUpHandler += LevelUp;
        target_angular = 0;
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
        if (flag_rot) {
            current_angular = Mathf.SmoothDamp(current_angular, target_angular, ref ref_angular, 0.8f);
            transform.Rotate(new Vector3(0, current_angular * Time.deltaTime, 0));
            flow_light.color = level_color[level.currentLevel()] * Mathf.Abs(Mathf.Sin(Mathf.PI * Time.time * level.currentLevel()));
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

    public void PlayRight (Transform trans) {
        Play(trans, right_feedback);
    }

    public void PlayWrong (Transform trans) {
        Play(trans, wrong_feedback);
    }

    void Play (Transform trans, GameObject feedback) {
        Vector3 pos = trans.position;
        Vector3 dir = pos.normalized;
        pos = pos - dir;
        GameObject instance = (GameObject)Instantiate(feedback, pos, Quaternion.identity);
        instance.transform.LookAt(Vector3.zero);
        vfx_list.Add(instance);
    }

    // Use this for initialization
    void FlowGenerate () {
        float radius = 600;
        float move = 2 * Mathf.PI / 60;
        for (float i = -1; i <= 1; i += 0.66f) {
            for (float theta = 0; theta < 2 * Mathf.PI; theta += move) {
                Vector3 pos = new Vector3(radius * Mathf.Cos(theta), radius * i * 1.5f, radius * Mathf.Sin(theta));
                GameObject obj = (GameObject)Instantiate(flow_unit, pos, Quaternion.identity);
                obj.transform.LookAt(Vector3.zero);
                obj.transform.parent = transform;
            }
        }
    }

    void LevelUp () {
        flow_light.color = level_color[level.currentLevel()];
        if (level.currentLevel() > 1) {
            flag_rot = true;
            target_angular = -Mathf.Sign(target_angular) * 10.0f * level.currentLevel();
        }
    }
}
