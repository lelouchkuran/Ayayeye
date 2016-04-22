using UnityEngine;
using System.Collections;

public class VFXHub : Singleton<VFXHub> {
    public GameObject tunnel_swipe;
    private Player player;

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<Player>().GetComponent<Player>();
    }

    public void TunnelSwipe () {
        GameObject current = player.GeneratorNow();
        GameObject swipe = Instantiate(tunnel_swipe);
        swipe.transform.parent = current.transform;
        swipe.transform.localPosition = Vector3.zero;
        swipe.transform.localRotation = Quaternion.identity;
        swipe.transform.localScale = new Vector3(1, 1, 1);
    }
}
