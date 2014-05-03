using UnityEngine;
using System.Collections;

public class MenuMainPlay : MonoBehaviour {

	void MenuMouseClick() {
		if(CheckpointController.instance) {
			Destroy(CheckpointController.instance);
			if(GameObject.Find("Checkpoint Controller")) {
				Destroy(GameObject.Find("Checkpoint Controller"));
			}
		}
		Application.LoadLevel("Siphon");
	}
}
