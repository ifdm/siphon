using UnityEngine;
using System.Collections;

public class JumpBreak : MonoBehaviour {

	public int health = 1;

	void OnCollisionEnter2D(Collision2D col) {
		if(col.collider.tag == "Player" && col.relativeVelocity.y > 8) {
			if(--health <= 0) {
				Destroy(gameObject);
			}
		}
	}
}
