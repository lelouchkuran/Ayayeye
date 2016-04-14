using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public GameObject StartGenerator;
	public GameObject FacingPoint;
	public Level level;

	CubeGenerator generator_now = null;
	CubeBehavior cube_now = null;
	public GameObject target = null;

	// Use this for initialization
	void Start () {
		generator_now = StartGenerator.GetComponent<CubeGenerator> ();
		generator_now.SetMoving (true);
		cube_now = generator_now.cube.GetComponent<CubeBehavior>();
		if (cube_now.GetDir () == 0) {
			target = null;
		} else {
			target = generator_now.neighbors[cube_now.GetDir () - 1];
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null)
			return;

		float angle_now = Vector3.Angle (FacingPoint.transform.position, generator_now.gameObject.transform.position);
		float angle_next = Vector3.Angle (FacingPoint.transform.position, target.transform.position);

		if (angle_next < Constant.Instance.AngleTurn || (angle_now > 90 - Constant.Instance.AngleTurn && angle_next < 40)) {
			Right ();
		} else {
			for (int i = 0; i < 4; ++i) {
				if (Vector3.Angle(FacingPoint.transform.position, generator_now.neighbors[i].transform.position) < Constant.Instance.AngleTurn) {
					Wrong(generator_now.neighbors[i]);
				}
			}
		}
	}

	public void Miss() {
		level.Wrong ();
		// wrong people effect
		NSwitch ();
		// miss people effect
	}

	public void Right() {
		level.Right ();
		cube_now.Finish (true);
        // right people effect
        if (target != null)
			SwitchG (target);
		else {
			NSwitch();
		}
	}

	public void Wrong(GameObject next_generator) {
		level.Wrong ();
		cube_now.Finish (false);
		// wrong people effect
		SwitchG (next_generator);
	}

	public void SwitchG(GameObject next_generator) {
		generator_now.SetMoving (false);
		generator_now = next_generator.GetComponent<CubeGenerator>();
		generator_now.SetMoving (true);
		cube_now = generator_now.cube.GetComponent<CubeBehavior>();
		if (cube_now.GetDir () == 0) {
			target = null;
		} else {
			target = generator_now.neighbors[cube_now.GetDir () - 1];
		}
	}

	public void NSwitch() {
		generator_now.Generate ();
		cube_now = generator_now.cube.GetComponent<CubeBehavior>();
		if (cube_now.GetDir () == 0) {
			target = null;
		} else {
			target = generator_now.neighbors[cube_now.GetDir () - 1];
		}
	}
}
