using UnityEngine;
using System.Collections;

public class CrushPlayer : MonoBehaviour {

	public void OnCollisionEnter2D(Collision2D col) {
		if(col.collider.tag == "Player") {
			Debug.Log(col.relativeVelocity);
			if(col.relativeVelocity.y < -3 && rigidbody2D.velocity.magnitude > 1) {
				gameObject.AddComponent("TouchOfDeath");
			}
		}
	}
}
