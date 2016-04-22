using UnityEngine;
using System.Collections;
using System;

public class InputController : MonoBehaviour {

    public Texture pressedMaterial;
    public Texture correctMaterial;
    public Texture unpressedMaterial;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", unpressedMaterial);
        transform.GetChild(2).gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", unpressedMaterial);
        transform.GetChild(3).gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", unpressedMaterial);
        transform.GetChild(4).gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", unpressedMaterial);


        if (Input.GetKey(Constant.Instance.KeyMap[0]))
        {
            Debug.Log("O");
            transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", pressedMaterial);

            transform.GetChild(2).gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", unpressedMaterial);
            transform.GetChild(3).gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", unpressedMaterial);
            transform.GetChild(4).gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", unpressedMaterial);

        }
        if (Input.GetKey(Constant.Instance.KeyMap[1]))
        {
            Debug.Log("X");
            transform.GetChild(4).gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", pressedMaterial);

            transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", unpressedMaterial);
            transform.GetChild(2).gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", unpressedMaterial);
            transform.GetChild(3).gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", unpressedMaterial);

        }
        if (Input.GetKey(Constant.Instance.KeyMap[2]))
        {
            Debug.Log("T");
            transform.GetChild(3).gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", pressedMaterial);

            transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", unpressedMaterial);
            transform.GetChild(2).gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", unpressedMaterial);
            transform.GetChild(4).gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", unpressedMaterial);

        }
        if (Input.GetKey(Constant.Instance.KeyMap[3]))
        {
            Debug.Log("S");
            transform.GetChild(2).gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", pressedMaterial);

            transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", unpressedMaterial);
            transform.GetChild(3).gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", unpressedMaterial);
            transform.GetChild(4).gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", unpressedMaterial);

        }
    }
}
