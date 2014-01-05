using UnityEngine;
using System.Collections;

public class FlyPath : MonoBehaviour {

	public Vector2 velocity = new Vector2(-2, 0);
	private bool porting = false;
	private bool entered = false;

	void Start() {
	}
	
	void FixedUpdate() {
		rigidbody2D.velocity = velocity;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Portal") {
			Porter porter = col.gameObject.GetComponent<Porter>();
			porter.Port(gameObject);
		}
		else if(!entered && !porting) {
			if(col.gameObject.tag == "Ground") {
				velocity = -velocity;
				entered = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.tag == "Ground") {
			entered = false;
		}
	}
}
