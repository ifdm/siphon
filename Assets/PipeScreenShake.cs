using UnityEngine;
using System.Collections;

public class PipeScreenShake : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.name == "Elbow Pipe") {
			CameraFollow follow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
			follow.shake = .3f;
			follow.shakeStrength = 1.6f;
		}
	}
}
