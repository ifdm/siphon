using UnityEngine;
using System.Collections;

public class FirstEncounterTrigger : MonoBehaviour {

	[HideInInspector]
	public bool triggered = false;

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			// Pressable
			triggered = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			// Not Pressable
			triggered = false;
		}
	}
}
