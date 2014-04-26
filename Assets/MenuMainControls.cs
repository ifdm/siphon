using UnityEngine;
using System.Collections;

public class MenuMainControls : MonoBehaviour {

	void MenuMouseClick() {
		SendMessageUpwards("MenuChangePage", "Controls");
	}
}
