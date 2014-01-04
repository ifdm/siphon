using UnityEngine;
using System.Collections;

public class Bounceable : MonoBehaviour {

	[HideInInspector] public bool bounced = false;

	void Bounce() {
		bounced = true;
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag == "Ground") {
			bounced = false;
		}
	}
}
