using UnityEngine;
using System.Collections;

public class NoClipLine : MonoBehaviour {

	public GameObject line;

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag == "Player") {
			if(col.gameObject.rigidbody2D.velocity.y > 0) {
				col.gameObject.layer = LayerMask.NameToLayer("Ghost Player");
			}
		}
	}
	
	void OnCollisionExit2D(Collision2D col) {
		if(col.gameObject.tag == "Player") {
			col.gameObject.layer = LayerMask.NameToLayer("Player");
		}
	}
}
