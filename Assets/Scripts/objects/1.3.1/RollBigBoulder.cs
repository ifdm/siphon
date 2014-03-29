using UnityEngine;
using System.Collections;

public class RollBigBoulder : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			GameObject.Find("Boulder").rigidbody2D.isKinematic = false;
		}
	}
}
