using UnityEngine;
using System.Collections;

public class CubeGenerator : MonoBehaviour {
	public bool no_words;
	public GameObject[] neighbors;
	public UseLessCubeDirection[] GenerateList;

	Vector3[] arrow_rotations;

	public Level level;

	public GameObject cube = null;
	int generatr_pointer = 0;
	bool moving = false;

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
			Debug.Log("set moving false will cube still in!!");
			Destroy(cube);
		}
		moving = _moving;

		if (moving) {
			Generate();
		}
	}

	public void Generate() {
		Debug.Log(gameObject.name + " generate!");
		int dir = GetDirection (), show_dir = dir;
		bool is_word = no_words ? false : level.IsWord ();
		bool is_oppo = level.IsOppo ();

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
		cube.transform.parent = transform;
		if (dir > 0 && is_word == false) {
			cube.transform.localRotation = Quaternion.Euler (arrow_rotations [show_dir - 1]);
		} else {
			cube.transform.localRotation = Quaternion.identity;
		}
		cube.transform.localPosition = Vector3.forward * Constant.Instance.CubeGenerateDis;
		cube.GetComponent<CubeBehavior> ().SetInfo (dir, show_dir, is_word);

		if (is_oppo && dir > 0) {
			cube.GetComponent<CubeBehavior> ().SetColor (Constant.Instance.OpposiColor);
		} else {
			cube.GetComponent<CubeBehavior> ().SetColor (Constant.Instance.NormalColor);
		}
	}

	int GetDirection() {
		if (generatr_pointer < GenerateList.Length) {
			generatr_pointer += 1;
			return Directions.ToInt(GenerateList[generatr_pointer - 1]);
		}

		// TODO
		return Random.Range (0, 5);
	}
}
