using UnityEngine;
using System.Collections;

public class PlayerEnabled : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D col) {
		var lift = transform.parent.GetComponent<Lift>();
		if(col.gameObject.tag == "Player" && lift.playerEnabled && !lift.running) {
			lift.running = true;
			lift.speed = lift.cachedSpeed;
		}
	}
}