using UnityEngine;
using System.Collections;

public class ChangeFootParticle : MonoBehaviour {

	public string type;

	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "Player") {
			col.transform.Find("Animation").GetComponent<PlayerAnimator>().particleType = type;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if(col.tag == "Player") {
			col.transform.Find("Animation").GetComponent<PlayerAnimator>().particleType = "dust";
		}
	}
}
