using UnityEngine;
using System.Collections;

public class RemoveHinge : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		var hinge = col.gameObject.GetComponent<HingeJoint2D>();
		if(hinge) {
			Destroy(col.gameObject.GetComponent<HingeJoint2D>());
		}
	}
}
