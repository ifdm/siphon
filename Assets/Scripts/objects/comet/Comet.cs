using UnityEngine;
using System.Collections;

public class Comet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		rigidbody2D.velocity = new Vector2(-38, -30);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.name == "FallingTree") {
			GameObject.Find("Main Camera").GetComponent<CameraFollow>().shake = .2f;
			Destroy(gameObject);
		}
	}
}
