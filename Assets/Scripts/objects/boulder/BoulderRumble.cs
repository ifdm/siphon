using UnityEngine;
using System.Collections;

public class BoulderRumble : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col) {
		if(col.relativeVelocity.magnitude > 10) {
			CameraFollow cam = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
			cam.shake = (rigidbody2D.velocity.magnitude - 10) / 20;
			cam.shakeStrength = rigidbody2D.velocity.magnitude - 8;
			GetComponent<EntityAudio>().Play("thud");
		}
	}
}
