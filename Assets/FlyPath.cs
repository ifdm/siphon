using UnityEngine;
using System.Collections;

public class FlyPath : MonoBehaviour {

	public float velocity = 2;
	private bool entered = false;

	void Start () {

	}
	
	void FixedUpdate () {
		rigidbody2D.velocity = new Vector2(velocity, rigidbody2D.velocity.y);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Portal") {
			Debug.Log("Teleporting.");

			Porter porter = col.gameObject.GetComponent<Porter>();
			porter.Port(gameObject);
		}
		else if(col.gameObject.name == "Player") {
			// Ignore.
		}
		else if(!entered) {
			Debug.Log("Turning around.");
			velocity = -velocity;
			entered = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		entered = false;
	}
}
