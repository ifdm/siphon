using UnityEngine;
using System.Collections;

public class Capture : MonoBehaviour {

	private GameObject item;
	private bool captured;
	private Vector2 collidedVelocity;
	// Use this for initialization
	void Start () {
		captured = false;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.name == "Fly") {
			if(!captured) {
				Catch(col.gameObject);
			}	
		}
		else if(col.gameObject.name == "Player") {
			if(captured) {
				Release(item);
			}
		}
	}
	
	void Release(GameObject other) {
		if(other.rigidbody2D) {
			FlyPath path = other.GetComponent<FlyPath>();
			if(path) {
				captured = false;
				path.velocity = new Vector2(collidedVelocity.x > 0 ? collidedVelocity.x : -collidedVelocity.x, collidedVelocity.y);
				other.renderer.enabled = true;
			}
		}
	}
	
	void Catch(GameObject other) {
		if(other.rigidbody2D) {
			FlyPath path = other.GetComponent<FlyPath>();
			if(path) {
				captured = true;
				// Save velocity
				collidedVelocity = path.velocity;
				// Stop it
				path.velocity = new Vector2(0, 0);
				// Hide it
				other.renderer.enabled = false;
				// Save it
				item = other;
			}
		}
	}
}
