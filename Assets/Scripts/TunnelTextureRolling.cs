using UnityEngine;
using System.Collections;

public class TunnelTextureRolling : MonoBehaviour {
	public TunnelOffsetSpeedController speed_controller;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 offset = GetComponent<Renderer> ().material.mainTextureOffset + new Vector2(speed_controller.GetSpeed () * Time.deltaTime, 0);
		GetComponent<Renderer> ().material.SetTextureOffset ("_MainTex", offset);
	}
}
