using UnityEngine;
using System.Collections;

public class MenuOptionsMusic : MonoBehaviour {

	void MenuToggleState(bool activated) {
		if(activated) {
			AudioListener.pause = false;
		}
		else {
			AudioListener.pause = true;
		}
	}
}
