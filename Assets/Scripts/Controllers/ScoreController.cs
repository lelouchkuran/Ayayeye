using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {

    public Material rightMaterial;
    public Material wrongMaterial;

	// Use this for initialization
	void Start () {

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
}
