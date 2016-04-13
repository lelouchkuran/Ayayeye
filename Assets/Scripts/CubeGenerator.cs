using UnityEngine;
using System.Collections;

public class CubeGenerator : MonoBehaviour {
	public GameObject neighborUp, neighborDown, neighborLeft, neighborRight;
	public UseLessCubeDirection[] GenerateList;

	public GameObject ArrowPrefab;
	Vector3[] arrow_rotations;
	public GameObject[] DirPrefab;
	public GameObject[] ShapePrefab;

	public Level level;

	GameObject cube = null;
	int generatr_pointer = 0;
	bool moving = false;

	// Use this for initialization
	void Awake () {
		arrow_rotations = new Vector3[4];
		arrow_rotations[0] = Vector3.zero;
		arrow_rotations[1] = new Vector3(0, 0, -90);
		arrow_rotations[2] = new Vector3(0, 0, 90);
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

	void Generate() {
		int dir = GetDirection ();
		bool is_word = level.IsWord ();
		bool is_oppo = level.IsOppo ();

		if (dir == 0) {
			cube = Instantiate (ShapePrefab [Random.Range (0, 4)]);
		} else {
			int show_dir = is_oppo ? Directions.GetOpposite(dir) : dir;
			if (is_word) {
				cube = Instantiate (DirPrefab [show_dir - 1]);
			} else {
				cube = Instantiate (ArrowPrefab);
				cube.transform.localRotation = Quaternion.Euler(arrow_rotations[show_dir - 1]);
			}
		}
		cube.transform.localPosition = Vector3.forward * Constant.Instance.CubeGenerateDis;
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
