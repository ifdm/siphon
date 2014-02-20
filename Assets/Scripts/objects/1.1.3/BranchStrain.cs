using UnityEngine;
using System.Collections;

public class BranchStrain : MonoBehaviour {

	// Use this for initialization
	void OnCollisionEnter2D(Collision2D col) {
		if(col.collider.tag == "Player") {
			rigidbody2D.mass = 10f;
		}
	}
}
