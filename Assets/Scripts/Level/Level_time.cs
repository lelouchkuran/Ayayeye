using UnityEngine;
using System.Collections;

public class Level_time : LevelBase {
	public TimerController time_controller;
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
		level_now = levels.Length - (int)(time_controller.GetTimePercent () * levels.Length) - 1;
		Debug.Log ("level " + level_now);
		if (level_now >= levels.Length) {
			level_now = levels.Length - 1;
			Debug.Log("END!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
			Application.LoadLevel("End");
			// TODO: finish

		}
        OnLevelUp();
	}
}