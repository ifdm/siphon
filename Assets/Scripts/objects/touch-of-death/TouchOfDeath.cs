using UnityEngine;
using System.Collections;

public class TouchOfDeath : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			PlayerControl control = col.gameObject.GetComponent<PlayerControl>();
			if(control.state != PlayerState.Dying){control.ChangeState(PlayerState.Dying);}
			Destroy(gameObject);
		}
	}
}
