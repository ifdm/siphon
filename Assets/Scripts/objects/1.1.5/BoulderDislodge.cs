using UnityEngine;
using System.Collections;

public class BoulderDislodge : MonoBehaviour {

	public GameObject boulder;
	public float force;
	public float delay;

	private bool pushed = false;
	
	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Player" && !pushed) {
			StartCoroutine(Push(boulder, force, delay));
		}
	}

	IEnumerator Push(GameObject boulder, float force, float delay) {
		yield return new WaitForSeconds(delay);
		pushed = true;
		boulder.rigidbody2D.AddForce(new Vector2(force, 0));
	}
}
