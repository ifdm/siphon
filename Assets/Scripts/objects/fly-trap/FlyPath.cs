using UnityEngine;
using System.Collections;

public class FlyPath : MonoBehaviour {

	public Vector2 velocity = new Vector2(-2, 0);
	private bool entered = false;
	
	void FixedUpdate() {
		rigidbody2D.velocity = velocity;
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		if(!entered) {
			if(col.gameObject.tag == "Ground") {
				velocity = -velocity;
				entered = true;
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D col) {
		entered = false;
	}
}
