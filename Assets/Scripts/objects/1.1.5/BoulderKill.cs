using UnityEngine;
using System.Collections;

public class BoulderKill : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.name == "Rootbot") {
			var animator = col.transform.Find("Animation").GetComponent<RootbotAnimator>();
			animator.One("Die");

			StartCoroutine(Kill(col.gameObject, 2.0f));
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.name == "Root") {
			Destroy(col.gameObject);
			Destroy(gameObject);
		}
	}

	IEnumerator Kill(GameObject gameObject, float delay) {
		yield return new WaitForSeconds(delay);
		Destroy(gameObject);
	}
	
}
