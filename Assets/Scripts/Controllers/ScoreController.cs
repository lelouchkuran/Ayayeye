using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {

//    public Material rightMaterial;
//    public Material wrongMaterial;
//    private int _startTime;

    public int totalScore = 0;
    public GameObject scoreGameObject;
	public NumGenerator num_generator;
	public GameObject score_generate, score_target;

	int combo_num = 0;

    void Awake ()
    {
        transform.parent = null;
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start () {
    }

    void OnLevelWasLoaded(int level)
    {
        if (level == 2)
        {
            Debug.Log("Total Score " + totalScore);
        }
    }

    // Update is called once per frame
    void Update () {	    
	}

    //public void setGrid (int cellNumber, bool isTrue)
    //{
    //    foreach (Transform plane in gameObject.transform)
    //    {
    //        foreach (Transform eachBlock in plane)
    //        {
    //            if (eachBlock.gameObject.name == cellNumber.ToString() && isTrue)
    //            {
    //                eachBlock.gameObject.GetComponent<Renderer>().material = rightMaterial;
    //            }
    //            else if (eachBlock.gameObject.name == cellNumber.ToString() && !isTrue)
    //            {
    //                eachBlock.gameObject.GetComponent<Renderer>().material = wrongMaterial;
    //            }
    //        }
    //    }
    //}

    // Set score
    public void setScore (bool isRight, int level) {
		int score_now;
        if (isRight) {
			combo_num++;
			score_now = (level + 1) * Constant.Instance.ScoreBase * combo_num;
			num_generator.Generate(score_now, score_generate.transform.position, score_target);
			totalScore += score_now;
        } else {
			combo_num = 0;
			score_now = (level + 1) * Constant.Instance.ScoreBase;
			totalScore -= score_now;
			scoreGameObject.GetComponent<ScoreShow>().Change(totalScore);
        }

        PlayerPrefs.SetInt("score", totalScore);
    }
}
