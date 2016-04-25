﻿using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

    public delegate void LevelInHandler ();
    public delegate void LevelOutHandler ();
    public event LevelInHandler LevelIn;
    public event LevelOutHandler LevelOut;

	public BaseLevel[] levels;
	int p, turns;
    static int count = -1;

	public TunnelOffsetSpeedController rolling_speed;
	public ScoreController score_controller;
	public VFXHub vfx;
    public SFXHub sfx;

    // Use this for initialization
    void Awake () {
		p = 0;
		turns = levels [p].turn_num;
    }

    public int currentLevel ()
    {
        return p;
    }

	public float GetSpeed() {
		return levels [p].speed_range.x + (levels [p].speed_range.y - levels [p].speed_range.x) * (turns / levels [p].turn_num);
	}

	public bool IsWord() {
		return (Random.Range(0.0f, 1.0f) < levels[p].word_ratio);
	}

	public bool IsOppo() {
		return (Random.Range(0.0f, 1.0f) < levels[p].oppo_ratio);
	}

/*
 * this is for cover type at first, but no more cover type now
	public int CoverInfo() {
		// 0 no voer, 1 all cover, 2 line cover -, 3 line cover |
		if (Random.Range (0.0f, 1.0f) < levels [p].cover_ratio) {
			if (Random.Range (0.0f, 1.0f) < levels [p].cover_all_ratio) {
				return 1;
			} else {
				return 2;
			}
		}
		return 0;
	}
*/
	public bool IsCover() {
		if (Random.Range (0.0f, 1.0f) < levels [p].cover_ratio) {
			return true;
		}
		return false;
	}

	public bool IsShape() {
		if (Random.Range (0.0f, 1.0f) < levels [p].shape_ratio) {
			return true;
		}
		return false;
	}

	public void Right(bool is_press) {
        count++;
		Debug.Log("R! ");
		//score_controller.setGrid(count, true);
		score_controller.setScore(true, p);
		if (!is_press) {
			Finish ();
		}
		rolling_speed.Right ();
		vfx.TunnelSwipe ();
        sfx.changePitch(PitchClass.Pitch.IncreasePitch);
    }

	public void Wrong() {
        count++;
		Debug.Log("W!");
		// score_controller.setGrid(count, false);
		score_controller.setScore(false, p);
        Finish();
		rolling_speed.Wrong ();
        sfx.changePitch(PitchClass.Pitch.ReducePitch);
    }

    void Finish() {
		turns -= 1;
		if (turns == 0) {
			p += 1;
			if (p >= levels.Length) {
				p = levels.Length - 1;
				Debug.Log("END!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                Application.LoadLevel("End");
				// TODO: finish

			}
			turns = levels[p].turn_num;
		}
	}
    // TODO
    public void TransLevel () {
        if (LevelOut != null)
            LevelOut();
        // TODO
        if (LevelIn != null)
            LevelIn();
    }

}

[System.Serializable]
public class BaseLevel {
	public float oppo_ratio;
	public float word_ratio;
	public float cover_ratio;
	public float shape_ratio;
	public Vector2 speed_range;
	public int turn_num;
}
