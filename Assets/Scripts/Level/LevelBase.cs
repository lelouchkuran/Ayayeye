using UnityEngine;
using System.Collections;

public class LevelBase : MonoBehaviour {

    public BaseLevel[] levels;
    protected int turns;

    public TunnelOffsetSpeedController rolling_speed;
    public ScoreController score_controller;
    public VFXHub vfx;
    public SFXHub sfx;
    public delegate void LevelUp ();
    public event LevelUp LevelUpHandler;
    virtual public int currentLevel () {
        return 0;
    }

    virtual public float GetSpeed () {
        return 0;
    }

    virtual public bool IsWord () {
        return false;
    }

    virtual public bool IsOppo () {
        return false;
    }

    virtual public bool IsCover () {
        return false;
    }

    virtual public bool IsShape () {
        return false;
    }

    virtual public void Right (bool is_press) {
        Debug.Log("R!");
        score_controller.setScore(true, 1);
        if (!is_press) {
            Finish();
        }
        rolling_speed.Right();
        vfx.TunnelSwipe();
        sfx.changePitch(PitchClass.Pitch.IncreasePitch);
    }

    virtual public void Wrong () {
        Debug.Log("W!");
        score_controller.setScore(false, 1);
        Finish();
        rolling_speed.Wrong();
        sfx.changePitch(PitchClass.Pitch.ReducePitch);
    }

    virtual protected void Finish () {
        turns -= 1;
    }

    protected void OnLevelUp () {
        if (LevelUpHandler != null)
            LevelUpHandler();
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
