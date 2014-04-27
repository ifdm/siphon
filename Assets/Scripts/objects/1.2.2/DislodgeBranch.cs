using UnityEngine;
using System.Collections;

public class DislodgeBranch : MonoBehaviour {

	private bool dirty = false;

	void Update() {
		if(!dirty) {
			foreach(Transform child in transform) {
				// Each trigger contains a script that keeps triggered state
				FirstEncounterTrigger script = child.gameObject.GetComponent<FirstEncounterTrigger>();
				// Check if we have a child with the script and a triggered state
				if(script && script.triggered) {
					StartCoroutine(dislodge());
					dirty = true;
				}
			}
		}
	}

	IEnumerator dislodge() {
		yield return new WaitForSeconds(2.3f);
		GameObject branch = GameObject.Find("Broken Branch");
		branch.rigidbody2D.isKinematic = false;
		branch.transform.Find("body").GetComponent<BoxCollider2D>().isTrigger = true;
		branch.transform.Find("leftEdge").GetComponent<BoxCollider2D>().isTrigger = true;
		branch.transform.Find("rightEdge").GetComponent<BoxCollider2D>().isTrigger = true;
		branch.transform.Find("bottomEdge").GetComponent<BoxCollider2D>().isTrigger = true;
		yield return new WaitForSeconds(1.7f);
		GameObject.Find("Main Camera").GetComponent<CameraFollow>().shake = .5f;
		GameObject.Find("Main Camera").GetComponent<CameraFollow>().shakeStrength = 2;
	}
}
