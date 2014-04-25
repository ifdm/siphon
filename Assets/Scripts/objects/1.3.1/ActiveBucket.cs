using UnityEngine;
using System.Collections;

public class ActiveBucket : MonoBehaviour {

	ActiveBucket sibling;
	SliderJoint2D joint;

	void Start() {
		joint = gameObject.GetComponent<SliderJoint2D>();

		foreach(Transform child in transform.parent) {
			if(
				child.name != gameObject.name && 
				(child.name != "Anchor Left" && child.name != "Anchor Right")
			) {
				sibling = child.gameObject.GetComponent<ActiveBucket>();
				break;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Player") down(false);
	}

	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.tag == "Player") up(false);
	}

	public void up(bool external) {
		toggle(Mathf.Abs(joint.motor.motorSpeed));
		if(!external) sibling.down(true);
	}

	public void down(bool external) {
		toggle(-Mathf.Abs(joint.motor.motorSpeed));
		if(!external) sibling.up(true);
	}

	void toggle(float speed) {
		var motor = joint.motor;
		motor.motorSpeed = speed; 

		joint.motor = motor;
	}

}
