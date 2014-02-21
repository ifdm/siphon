using UnityEngine;
using System.Collections;

public class TreeFall : MonoBehaviour {

	private bool fallen = false;

	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "Player" && !fallen) {
			StartCoroutine(treeFall());
			fallen = true;
		}
	}

	IEnumerator treeFall() {
		yield return new WaitForSeconds(1.0f);
		GameObject fallingTree = GameObject.Find("FallingTree");
		fallingTree.rigidbody2D.isKinematic = false;
		fallingTree.rigidbody2D.AddForceAtPosition(new Vector2(100, 0), new Vector2(0, -2));
		fallingTree.AddComponent("TouchOfDeath");
		StartCoroutine(makeSafe());
		GameObject.Find("Comet").AddComponent("Comet");
	}

	IEnumerator makeSafe() {
		yield return new WaitForSeconds(4.0f);
		GameObject fallingTree = GameObject.Find("FallingTree");
		Destroy(fallingTree.GetComponent<TouchOfDeath>());
	}
}
