using UnityEngine;
using System.Collections;

public class DislodgeBranch : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "Player") {
			GameObject branch = GameObject.Find("Broken Branch");
			branch.rigidbody2D.isKinematic = false;
			branch.transform.Find("body").GetComponent<BoxCollider2D>().isTrigger = true;
			branch.transform.Find("leftEdge").GetComponent<BoxCollider2D>().isTrigger = true;
			branch.transform.Find("rightEdge").GetComponent<BoxCollider2D>().isTrigger = true;
			branch.transform.Find("bottomEdge").GetComponent<BoxCollider2D>().isTrigger = true;
		}
	}
}
