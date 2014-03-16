using UnityEngine;
using System.Collections;

public class CrushPlayer : MonoBehaviour {

	public float velocityThreshold = 0.6f;

	public void OnCollisionEnter2D(Collision2D col) {
		if(col.collider.tag == "Player") {
			Debug.Log(col.relativeVelocity);
			if(col.relativeVelocity.y < -3 && rigidbody2D.velocity.magnitude > velocityThreshold) {
				TouchOfDeath touchOfDeath = (TouchOfDeath)gameObject.AddComponent("TouchOfDeath");
				if(Mathf.Sign(col.relativeVelocity.x) == Mathf.Sign(col.transform.lossyScale.x)) {
					touchOfDeath.animation = TouchOfDeath.DeathAnimation.backHit;
				}
				else {
					touchOfDeath.animation = TouchOfDeath.DeathAnimation.frontHit;
				}
			}
		}
	}
}
