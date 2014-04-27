using UnityEngine;
using System.Collections;

public class ScreenShakeArea : MonoBehaviour {

	public float strength = .2f;

	void OnTriggerStay2D(Collider2D col) {
		CameraFollow follow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
		if(follow.shake > 0) {
			if(col.gameObject.name == "Player") {
				follow.shake = Mathf.Max(follow.shake, .3f);
				follow.shakeStrength = strength;
			}
		}
	}
}
