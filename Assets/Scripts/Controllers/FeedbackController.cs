using UnityEngine;
using System.Collections;

public class FeedbackController : MonoBehaviour {

    public GameObject rightFeedback;
    public Transform target;
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
                if (explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("End"))
                {
                    _explosionArray.Remove(explosion);
                    Destroy(explosion);
                }
            }
        }
    }

    public void playRightFeedback (Transform right_transform)
    {
        _explosion = Instantiate(rightFeedback, right_transform.position, Quaternion.identity) as GameObject;
        _explosion.transform.parent = right_transform.parent;
        _explosion.transform.LookAt(target);
        _explosionArray.Add(_explosion);
    }
}
