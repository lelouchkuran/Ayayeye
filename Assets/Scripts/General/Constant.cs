using UnityEngine;
using System.Collections;

public class Constant : Singleton<Constant> {
	public float CubeGenerateDis;
	public float CubeMissDis;

	public float AngleTurn;
	public Color NormalColor;
	public Color OpposiColor;

    public float CoverStepL = 0.3f;
    public float CoverCD = 0.0f;
    public float CoverOffset = -0.75f;

    //Popping
	public float dis_mov;
	public float scale;
	public float time_come;
	public float time_down;

	// tunnal color feedback
	public int FlashTimes_Right;
	public float FlashPeriod_Right;
	public float FlashStay_Right;
	public int FlashTimes_Wrong;
	public float FlashPeriod_Wrong;
	public float FlashStay_Wrong;
	public Color ColorNormal;
	public Color ColorRight;
	public Color ColorWrong;

	// shake
	public float ShakeTime;
	public float ShakeRange;

	public float SpeedIncForOffset;

	public int ScoreBase;

    #region prefabs
    public GameObject ArrowPrefab;
	public GameObject[] DirPrefab;
	public GameObject[] ShapePrefab;
	public KeyCode[] KeyMap;
	#endregion
}

[System.Serializable]
public enum UseLessCubeDirection {
	Stay = 0,
	Up = 1,
	Down = 4,
	Left = 2,
	Right = 3
}

public class Directions {
	// get relate
	static public UseLessCubeDirection GetOpposite(UseLessCubeDirection dir) {
		if (dir == UseLessCubeDirection.Up) {
			return UseLessCubeDirection.Down;
		}
		if (dir == UseLessCubeDirection.Down) {
			return UseLessCubeDirection.Up;
		}
		if (dir == UseLessCubeDirection.Left) {
			return UseLessCubeDirection.Right;
		}
		return UseLessCubeDirection.Left;
	}
	static public int GetOpposite(int dir) {
		return  5 - dir;
	}

	// trans
	static public UseLessCubeDirection ToDir(int _type) {
		if (_type == 1) {
			return UseLessCubeDirection.Up;
		} else if (_type == 4) {
			return UseLessCubeDirection.Down;
		} else if (_type == 2) {
			return UseLessCubeDirection.Left;
		} else if (_type == 3) {
			return UseLessCubeDirection.Right;
		}
		return UseLessCubeDirection.Stay;
	}
	static public int ToInt(UseLessCubeDirection _dir) {
		if (_dir == UseLessCubeDirection.Up) {
			return 1;
		} else if (_dir == UseLessCubeDirection.Down) {
			return 4;
		} else if (_dir == UseLessCubeDirection.Left) {
			return 2;
		} else if (_dir == UseLessCubeDirection.Right) {
			return 3;
		}
		return 0;
	}
}