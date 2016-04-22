using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {

    public Material rightMaterial;
    public Material wrongMaterial;

    private float _startTime;

    public int totalScore = 0;
    public const int worstTime = 5;
    public int multiplier = 12;

    public GameObject playerObject;
    public GameObject scoreGameObject;
	public NumGenerator num_generator;
    public GameObject score_generate, score_target;
    public GameObject[] particleSystemArray;

    GameObject currentParticle;

    public int bonusMultiplier = 4;
    int _currentMultiplier = 0;

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
        if (currentParticle &&
            !currentParticle.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().isPlaying)
        {
            foreach (Transform eachParticle in currentParticle.transform)
            {
                eachParticle.gameObject.GetComponent<ParticleSystem>().Stop();
            }
        }
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

        if (currentParticle)
        {
            foreach (Transform eachParticle in currentParticle.transform)
            {
                eachParticle.gameObject.GetComponent<ParticleSystem>().Stop();
            }
        }

        //GameObject particle = playerObject.GetComponent<Player>().generator_now.transform.GetChild(1).gameObject as GameObject;
        //Debug.Log("asfnaopsif " + particle.name);

        // Increase score
        if (isRight)
        {
            _currentMultiplier++;
            scoreForRound = (worstTime - timeTaken) * multiplier;
            num_generator.Generate((int)scoreForRound, score_generate.transform.position, score_target);
            totalScore += (int)scoreForRound;
        }
        else
        {
            _currentMultiplier = 0;
            scoreForRound = (worstTime - timeTaken) * 2;
            totalScore -= (int)scoreForRound;
        }
        
        // Bonus Multiplier
        if (_currentMultiplier >= bonusMultiplier)
        {
            _currentMultiplier = 0;
            //Debug.Log("iafhoaifh " + playerObject.GetComponent<Player>().generator_now.name);
            currentParticle = playerObject.GetComponent<Player>().generator_now.transform.GetChild(1).gameObject as GameObject;
            currentParticle.SetActive(true);
            foreach (Transform eachParticle in currentParticle.transform)
            {
                eachParticle.gameObject.GetComponent<ParticleSystem>().Play();
            }

            Debug.Log("asfnaopsif " + currentParticle.name);
        }

        _startTime = previousTime;

        scoreGameObject.GetComponent<ScoreShow>().Change(totalScore);
    }
}
