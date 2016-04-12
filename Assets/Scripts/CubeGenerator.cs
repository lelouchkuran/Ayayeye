using UnityEngine;
using System.Collections;

public class CubeGenerator : MonoBehaviour {
	public GameObject neighborUp, neighborDown, neighborLeft, neighborRight;
	public UseLessCubeDirection[] GenerateList;

	GameObject cube = null;
	int generatr_pointer = 0;
	bool moving = false;

	// Use this for initialization
	void Start () {
	
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
