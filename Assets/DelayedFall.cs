using UnityEngine;
using System.Collections;

public class DelayedFall : MonoBehaviour {

	public float delay;
	public GameObject victim;

	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "Player") {
			StartCoroutine(destroy());
		}
	}

	IEnumerator destroy() {
		yield return new WaitForSeconds(delay);
		if(victim) {
			foreach(BoxCollider2D col in victim.GetComponentsInChildren<BoxCollider2D>()) {
				col.isTrigger = true;
			}
			victim.rigidbody2D.isKinematic = false;
		}
	}
}