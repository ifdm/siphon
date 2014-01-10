using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.gameObject.tag == "Player") {
			PlayerMovement.checkpoint = transform.position;
		}
	}
}
