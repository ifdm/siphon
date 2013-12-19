using UnityEngine;
using System.Collections;

public class TouchOfDeath : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
