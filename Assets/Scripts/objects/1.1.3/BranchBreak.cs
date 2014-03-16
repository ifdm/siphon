using UnityEngine;
using System.Collections;

public class BranchBreak : MonoBehaviour {

	// Use this for initialization
	void OnCollisionEnter2D(Collision2D col) {
		if(col.collider.tag == "Player") {
			GameObject branch = GameObject.Find("Branch1");
			if(branch) {
				HingeJoint2D joint = branch.GetComponent<HingeJoint2D>();
				if(joint) {
					Destroy(joint);
				}
			}
		}
	}
}
