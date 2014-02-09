using UnityEngine;
using System.Collections;

public class OneWay : MonoBehaviour {

	public float delay = 0.2f;
	private GameObject player;
	private bool inside;
	private bool grown = false;

	void Start() {
		player = GameObject.Find("Player");		
		StartCoroutine(Grown(delay));
	}

	IEnumerator Grown(float delay) {
		yield return new WaitForSeconds(delay);
		grown = true;
	}

	void Update() {
		if(grown) {
			if(player.rigidbody2D.velocity.y > 0 || inside) {
				gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
			}
			else {
				gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
			}
		}
		else {
			gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
		}

		if(player.GetComponent<PlayerControl>().state == PlayerState.Climbing) {
			inside = true;
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		if(col.gameObject.tag == "Player" && grown) {
			inside = true;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Player" && grown) {
			inside = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.tag == "Player" && grown) {
			if((col.gameObject.rigidbody2D.velocity.y > 0 && col is CircleCollider2D) || col is EdgeCollider2D) {
				inside = false;
			}
		}
	}
}
