using UnityEngine;
using System.Collections;

public class ScreenShakeArea : MonoBehaviour {

	void OnTriggerStay2D(Collider2D col) {
		CameraFollow follow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
		if(col.gameObject.name == "Player") {
			follow.shake = .3f;
			follow.shakeStrength = .2f;
		}
	}
}
