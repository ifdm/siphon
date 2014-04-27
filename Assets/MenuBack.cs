using UnityEngine;
using System.Collections;

public class MenuControlsBack : MonoBehaviour {

	void MenuMouseClick() {
		SendMessageUpwards("MenuChangePage", "Previous");
	}
}
