using UnityEngine;
using System.Collections;

public class NoClipLine : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			//if(col.gameObject.rigidbody2D.velocity.y > 0) {
				Debug.Log ("Ghost");
				col.gameObject.layer = LayerMask.NameToLayer("Ghost Player");
			//}
		}
	}
	
	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			Debug.Log ("Player");
			col.gameObject.layer = LayerMask.NameToLayer("Player");
		}
	}
}
