using UnityEngine;
using System.Collections;

public class BranchBreak : MonoBehaviour {

	// Use this for initialization
	void OnCollisionEnter2D(Collision2D col) {
		if(col.collider.tag == "Player") {
			GameObject branch = GameObject.Find("Branch2");
			if(branch) {
				HingeJoint2D joint = branch.GetComponent<HingeJoint2D>();
				if(joint) {
					Destroy(joint);
					StartCoroutine(ded());
				}
			}
		}
	}

	IEnumerator ded() {
		yield return new WaitForSeconds(1.5f);
		Destroy(GameObject.Find("Branch1"));
		Destroy(GameObject.Find("Branch2"));
		Destroy(GameObject.Find("Branch3"));
		Destroy(GameObject.Find("Branch4"));
	}
}
