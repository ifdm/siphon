using UnityEngine;
using System.Collections;

public class DelayedDestroy : MonoBehaviour {

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
			Destroy(victim);
		}
	}
}