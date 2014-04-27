using UnityEngine;
using System.Collections;

public class GiantBoulderShakeTrigger : MonoBehaviour {

	public float strength = 5;
	public float duration = .75f;
	private bool dirty = false;

	void OnTriggerEnter2D(Collider2D col) {
		if(!dirty && col.gameObject.name == "Rolling Boulder") {
			CameraFollow follow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
			follow.shake = duration;
			follow.shakeStrength = strength;
			dirty = true;
		}
	}
}
