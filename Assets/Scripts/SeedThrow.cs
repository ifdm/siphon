using UnityEngine;
using System.Collections;

public class SeedThrow : MonoBehaviour {

	// Use this for initialization
	void Start() {
		//rigidbody2D.AddForce(new Vector2(1000, 30)); 
	}
	
	void OnCollisionEnter2D (Collision2D col) {
		if(col.gameObject.tag == "Ground") {
			DestroyObject(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update() {
		
	}

	void launch(Vector3 src, Vector3 dst) {

	}
}
