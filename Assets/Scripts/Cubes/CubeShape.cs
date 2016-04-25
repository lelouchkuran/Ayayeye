using UnityEngine;
using System.Collections;

public class CubeShape : CubeBehavior {
	KeyCode key = KeyCode.A;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();

		if (key == KeyCode.A)
			return;
		if (Input.GetKeyDown (key)) {
			GameObject.Find ("Player").GetComponent<Player> ().Right (true);
            VFXHub.Instance.PlayRight(transform);
            SFXHub.Instance.playRightSound();
        }
        else if (Input.anyKeyDown){
            // TODO: button press wrong
            VFXHub.Instance.PlayWrong(transform);
            SFXHub.Instance.playWrongSound();
        }
    }

	public override void SetInfo(int _dir, int _dir_show, bool _word) {
		base.SetInfo (_dir, _dir_show, _word);
		key = Constant.Instance.KeyMap [_dir_show];
	}
}