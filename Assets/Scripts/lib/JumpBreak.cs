using UnityEngine;
using System.Collections;

public class JumpBreak : MonoBehaviour {

	public int health = 1;

	void OnCollisionEnter2D(Collision2D col) {
		if(col.collider.tag == "Player" && col.relativeVelocity.y > 8) {
			var animator = transform.Find("Animation").GetComponent<BraceAnimator>();

			if(--health <= 0) {
				GetComponent<EntityAudio>().One("Branch_Break");
				animator.Set("Break");
				Destroy(gameObject.GetComponent<BoxCollider2D>());
			}

			if(health == 1) {
				animator.Set("Crack");
			}
		}
	}
}
