﻿using UnityEngine;
using System.Collections;

public class CubeGenerator : MonoBehaviour {
	public bool no_words;
	public bool no_continue;
	
	public string[] tutorialWords;
	
	public GameObject[] neighbors;
	
	public UseLessCubeDirection[] GenerateList;
	
	Vector3[] arrow_rotations;
	
	public LevelBase level;
	
	public GameObject cube = null;
	int generatr_pointer = 0;
	bool moving = false;
	GameObject last_generater = null, text_obj = null;
	
	public Shake shake;
	
	// Use this for initialization
	void Awake () {
		arrow_rotations = new Vector3[4];
		arrow_rotations[0] = Vector3.zero;
		arrow_rotations[1] = new Vector3(0, 0, 90);
		arrow_rotations[2] = new Vector3(0, 0, -90);
		arrow_rotations[3] = new Vector3(0, 0, 180);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void SetMoving(bool _moving = true) {
		if (_moving == false && cube != null) {
			Destroy(cube);
		}
		moving = _moving;
		
		if (moving) {
			Generate();
		}
	}
	
	public void Generate() {
		// status
		int dir = GetDirection (), show_dir = dir;
		bool is_word = no_words ? false : level.IsWord ();
		bool is_oppo = level.IsOppo ();
		
		// generate
		if (dir == 0) {
			show_dir = Random.Range (0, 4);
			cube = Instantiate (Constant.Instance.ShapePrefab [show_dir]);
		} else {
			if (is_oppo) {
				show_dir = Directions.GetOpposite(dir);
			}
			if (is_word) {
				cube = Instantiate (Constant.Instance.DirPrefab [show_dir - 1]);
			} else {
				cube = Instantiate (Constant.Instance.ArrowPrefab);
			}
		}
		
		// rotate & info
		cube.transform.parent = transform;
		if (dir > 0 && is_word == false) {
			cube.transform.localRotation = Quaternion.Euler (arrow_rotations [show_dir - 1]);
		} else {
			cube.transform.localRotation = Quaternion.identity;
		}
		cube.transform.localPosition = Vector3.forward * Constant.Instance.CubeGenerateDis;
		cube.GetComponent<CubeBehavior> ().SetInfo (dir, show_dir, is_word);
		
		// set color
		if (is_oppo && dir > 0) {
			cube.GetComponent<CubeBehavior> ().SetColor (Constant.Instance.OpposiColor);
		} else {
			cube.GetComponent<CubeBehavior> ().SetColor (Constant.Instance.NormalColor);
		}
		
		// add tutorial
		TutorialAdd (dir == 0, is_oppo);
	}
	
	void TutorialAdd(bool is_shape, bool is_oppo) {
		// get tutorial text
		if (is_shape) {
			text_obj = level.Tutorial(0);
		} else {
			text_obj = level.Tutorial (1);
			if (is_oppo && text_obj == null) {
				text_obj = level.Tutorial (2);
			}
		}
		if (text_obj == null) {
			return;
		}

		// set position
		text_obj.transform.parent = transform;
		text_obj.transform.localPosition = Vector3.up * 2;
		text_obj.transform.localRotation = Quaternion.identity;
		text_obj.transform.parent = cube.transform;
		Vector3 pos = text_obj.transform.localPosition;
		pos.z = 0;
		text_obj.transform.localPosition = pos;
	}
	
	public void SetLast(GameObject _obj) {
		last_generater = _obj;
	}
	
	int GetDirection() {
		if (generatr_pointer < GenerateList.Length) {
			generatr_pointer += 1;
			return Directions.ToInt(GenerateList[generatr_pointer - 1]);
		}
		
		// TODO
		if (level.IsShape ()) {
			return 0;
		}
		int dir = Random.Range (1, 5);
		while (neighbors[4 - dir] == last_generater) {
			dir = Random.Range (1, 5);
		}
		return dir;
	}
}
