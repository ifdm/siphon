using UnityEngine;
using System.Collections;

public class MenuMainPlay : MonoBehaviour {

	void MenuMouseClick() {
		if(CheckpointController.instance) {
			Destroy(CheckpointController.instance);
			Destroy(CheckpointController.gameObject);
		}
		Application.LoadLevel("Siphon");
	}
}
