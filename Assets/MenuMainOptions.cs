using UnityEngine;
using System.Collections;

public class MenuMainOptions : MonoBehaviour {

	void MenuMouseClick() {
		SendMessageUpwards("MenuChangePage", "Options");
	}
}
