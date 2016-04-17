using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {

    public Material rightMaterial;
    public Material wrongMaterial;

    private float _startTime;

    public int totalScore = 0;
    public const int worstTime = 5;
    public int multiplier = 12;

    void Awake ()
    {
        transform.parent = null;
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start () {
        _startTime = Time.time;
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

    public void setGrid (int cellNumber, bool isTrue)
    {
        foreach (Transform plane in gameObject.transform)
        {
            foreach (Transform eachBlock in plane)
            {
                if (eachBlock.gameObject.name == cellNumber.ToString() && isTrue)
                {
                    eachBlock.gameObject.GetComponent<Renderer>().material = rightMaterial;
                }
                else if (eachBlock.gameObject.name == cellNumber.ToString() && !isTrue)
                {
                    eachBlock.gameObject.GetComponent<Renderer>().material = wrongMaterial;
                }
            }
        }
    }

    // Set score
    public void setScore (bool isRight, float previousTime)
    {
        float timeTaken = previousTime - _startTime;
        float scoreForRound;

        // Increase score
        if (isRight)
        {
            scoreForRound = (worstTime - timeTaken) * multiplier;
            totalScore += (int)scoreForRound;
        }
        else
        {
            scoreForRound = (worstTime - timeTaken) * 2;
            totalScore -= (int)scoreForRound;
        }

        _startTime = previousTime;
    }
}
