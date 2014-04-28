using UnityEngine;
using System.Collections;

public class BoulderKill : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.name == "Rootbot") {
			var animator = col.transform.Find("Animation").GetComponent<RootbotAnimator>();
			animator.One("Die");
			GetComponent<EntityAudio>().One("robotCrash");

			var particles = animator.GetComponentsInChildren<ParticleSystem>();
			foreach(ParticleSystem particle in particles) {
				particle.Stop();
			}

			Destroy(col.gameObject.GetComponent<CircleCollider2D>());
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.name == "Root") {
			Destroy(col.gameObject);
			Destroy(gameObject);
		}
	}
	
}
