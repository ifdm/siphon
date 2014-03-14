using UnityEngine;
using System.Collections;

public class BoulderKill : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.name == "RootKiller") {
			Destroy(col.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.name == "Root") {
			Destroy(col.gameObject);
			Destroy(gameObject);
		}
	}
}
