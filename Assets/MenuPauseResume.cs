using UnityEngine;
using System.Collections;

public class MenuPauseResume : MonoBehaviour {

	void MenuMouseClick() {
		SendMessageUpwards("TogglePause", null, SendMessageOptions.DontRequireReceiver);
	}
}
