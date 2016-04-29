using UnityEngine;
using System.Collections;

public class Level : LevelBase {
	// the level_now
	int level_now;
    // Use this for initialization
    void Awake () {
		level_now = 0;
		turns = levels [level_now].turn_num;
    }

    override public int currentLevel ()
    {
		return level_now;
    }

	override public float GetSpeed() {
		return levels [level_now].speed_range.x + (levels [level_now].speed_range.y - levels [level_now].speed_range.x) * (turns / levels [level_now].turn_num);
	}

	override public bool IsWord() {
		return (Random.Range(0.0f, 1.0f) < levels[level_now].word_ratio);
	}

	override public bool IsOppo() {
		return (Random.Range(0.0f, 1.0f) < levels[level_now].oppo_ratio);
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
	override public bool IsCover() {
		if (Random.Range (0.0f, 1.0f) < levels [level_now].cover_ratio) {
			return true;
		}
		return false;
	}

	override public bool IsShape() {
		if (Random.Range (0.0f, 1.0f) < levels [level_now].shape_ratio) {
			return true;
		}
		return false;
	}

	override public void Right(bool is_press) {
		Debug.Log("R! ");
		//score_controller.setGrid(count, true);
		score_controller.setScore(true, level_now);
		if (!is_press) {
			Finish ();
		}
		rolling_speed.Right ();
		vfx.TunnelSwipe ();
        sfx.changePitch(PitchClass.Pitch.IncreasePitch);
    }

	override public void Wrong() {
		Debug.Log("W!");
		// score_controller.setGrid(count, false);
		score_controller.setScore(false, level_now);
        Finish();
		rolling_speed.Wrong ();
        sfx.changePitch(PitchClass.Pitch.ReducePitch);
    }

	override protected void Finish() {
		turns -= 1;
		if (turns == 0) {
			level_now += 1;
			if (level_now >= levels.Length) {
				level_now = levels.Length - 1;
				Debug.Log("END!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                Application.LoadLevel("End");
				// TODO: finish

			}
			turns = levels[level_now].turn_num;
		}
	}
}