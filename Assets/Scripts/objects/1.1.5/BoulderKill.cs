﻿using UnityEngine;
using System.Collections;

public class BoulderKill : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.name == "Rootbot") {
			var animator = col.transform.Find("Animation").GetComponent<RootbotAnimator>();
			animator.One("Die");
			GetComponent<EntityAudio>().One("robotCrash");

			Destroy(col.gameObject.GetComponent<CircleCollider2D>());
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.name == "Root") {
			Destroy(col.gameObject);
			StartCoroutine(Kill (gameObject, 2.0f));
		}
	}
	
}
