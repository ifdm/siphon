using UnityEngine;
using System.Collections;

public class BoulderKill : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.name == "Rootbot") {
			var animator = col.transform.Find("Animation").GetComponent<RootbotAnimator>();
			animator.One("Die");
			GetComponent<EntityAudio>().One("robotCrash");
			StartCoroutine(Kill(col.gameObject, 4.0f));

		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.name == "Root") {
			Destroy(col.gameObject);
			StartCoroutine(Kill (gameObject, 2.0f));
		}
	}

	IEnumerator Kill(GameObject gameObject, float delay) {
		yield return new WaitForSeconds(delay);
		Destroy(gameObject);
	}
	
}
