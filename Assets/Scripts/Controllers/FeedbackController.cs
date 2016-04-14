using UnityEngine;
using System.Collections;

public class FeedbackController : MonoBehaviour {

    public GameObject rightFeedback;
    private GameObject _explosion;
    private ArrayList _explosionArray;
    // Use this for initialization
    void Start () {
        _explosionArray = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () {
        if (_explosionArray.Count > 0)
        {
            for (int i = 0; i <_explosionArray.Count; i++)
            {
                GameObject explosion = _explosionArray[i] as GameObject;
                if (!explosion.GetComponent<ParticleSystem>().isPlaying)
                {
                    _explosionArray.Remove(explosion);
                    Destroy(explosion);
                }
            }
        }
    }

    public void playRightFeedback (GameObject rightGameObject)
    {
        _explosion = Instantiate(rightFeedback, rightGameObject.transform.position, Quaternion.identity) as GameObject;
        _explosion.GetComponent<ParticleSystem>().Play();
        _explosionArray.Add(_explosion);
    }
}
