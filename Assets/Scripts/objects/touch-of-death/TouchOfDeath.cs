using UnityEngine;
using System.Collections;

public class TouchOfDeath : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			col.gameObject.GetComponent<PlayerControl>().ChangeState(PlayerState.Dying);
			Destroy(this);
		}
		else if(col.gameObject.tag == "Seed") {
			Destroy(col.gameObject);
		}
	}
}
