using UnityEngine;
using System.Collections;

public class Comet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		rigidbody2D.velocity = new Vector2(-25, -20);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.name == "Death") {
			GameObject.Find("Main Camera").GetComponent<CameraFollow>().shake = 2;
			Destroy(gameObject);
		}
	}
}
