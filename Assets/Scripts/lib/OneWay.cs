using UnityEngine;
using System.Collections;

public class OneWay : MonoBehaviour {

	private GameObject player;
	private bool inside;
	private float grace = 0.2f;

	void Start() {
		player = GameObject.Find("Player");
	}

	void Update() {
		grace -= Mathf.Min(grace, Time.deltaTime);
		if(player.rigidbody2D.velocity.y > 0 || inside || grace > 0) {
			Debug.Log("You can go through me.");
			gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
		}
		else {
			gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			inside = true;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			inside = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			inside = false;
		}
	}
}
