using UnityEngine;
using System.Collections;

public class MenuOptionsVSync : MonoBehaviour {

	void MenuToggleState(bool activated) {
		if(activated) {
			QualitySettings.vSyncCount = 1;
		}
		else {
			QualitySettings.vSyncCount = 0;
		}
	}
}
