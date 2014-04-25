using UnityEngine;
using System.Collections;

public class BranchBreak : MonoBehaviour {

	public GameObject branch;

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "Player" && col.gameObject.GetComponent<PlayerControl>().isGrounded() && branch) {
			HingeJoint2D joint = branch.GetComponent<HingeJoint2D>();
			EntityAudio audio = GetComponent<EntityAudio>();
			audio.One("Branch_Break");
			if(joint) {
				JointAngleLimits2D limits = joint.limits;
				limits.min = -5;
				limits.max = 5;
				joint.limits = limits;
				StartCoroutine(breakBranch());
			}
		}
	}

	IEnumerator breakBranch() {
		yield return new WaitForSeconds(.6f);
		branch.rigidbody2D.AddForceAtPosition(new Vector2(0, -100), new Vector2(4, 0));
		Destroy(branch.GetComponent<HingeJoint2D>());
		branch.GetComponent<BoxCollider2D>().isTrigger = true;
	}
}
