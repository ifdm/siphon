using UnityEngine;
using System.Collections;

public class Comet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		rigidbody2D.velocity = new Vector2(-38, -30);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.name == "FallingTree") {
			GameObject camera = GameObject.Find("Main Camera");
			camera.GetComponent<CameraFollow>().shake = 1.0f;
			camera.GetComponent<CameraFollow>().shakeStrength = 6.0f;
			rigidbody2D.velocity = Vector2.zero;
			Destroy(GetComponent<SpriteRenderer>());
		}
	}
}
