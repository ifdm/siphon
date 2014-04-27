using UnityEngine;
using System.Collections;

public class PillarShake : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col) {
		if(col.transform.parent.name == "PlatformShake") {
			GameObject.Find("Main Camera").GetComponent<CameraFollow>().shake = .7f;
			GameObject.Find("Main Camera").GetComponent<CameraFollow>().shakeStrength = 1.5f;
		}
	}
}
