using UnityEngine;
using System.Collections;

public class TreeImpact : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		Debug.Log(col.gameObject.name);
		if(col.gameObject.name == "FallingTree") {
			GameObject camera = GameObject.Find("Main Camera");
			camera.GetComponent<CameraFollow>().shake = .3f;
			camera.GetComponent<CameraFollow>().shakeStrength = 4.0f;
		}
	}
}
