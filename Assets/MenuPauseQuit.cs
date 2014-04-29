using UnityEngine;
using System.Collections;

public class MenuPauseQuit : MonoBehaviour {

	void MenuMouseClick() {
		Time.timeScale = 1;
		Application.LoadLevel("Menu");
	}
}
