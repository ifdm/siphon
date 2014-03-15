using UnityEngine;
using System.Collections;

public class TouchOfDeath : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			PlayerControl control = col.gameObject.GetComponent<PlayerControl>();
			if(control.state != PlayerState.Dying) {
				Destroy(col.gameObject.GetComponent<Rigidbody2D>());
				Destroy(col.gameObject.GetComponent<EdgeCollider2D>());
				Destroy(col.gameObject.GetComponent<BoxCollider2D>());
				Destroy(col.gameObject.GetComponent<CircleCollider2D>());
				control.ChangeState(PlayerState.Dying);
			}
		}
	}
}
