using UnityEngine;
using System.Collections;

public class MenuMainPlay : MonoBehaviour {

	void MenuMouseClick() {
		if(CheckpointController.instance) {
			CheckpointController.instance.position = Vector3.zero;
		}
		Application.LoadLevel("Siphon");
	}
}
