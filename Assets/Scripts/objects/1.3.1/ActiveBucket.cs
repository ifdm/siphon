using UnityEngine;
using System.Collections;

public class ActiveBucket : MonoBehaviour {

	ActiveBucket sibling;
	SliderJoint2D joint;
	private float targetSpeed;

	void Start() {
		joint = gameObject.GetComponent<SliderJoint2D>();
		targetSpeed = joint.motor.motorSpeed;

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

	void Update() {
		var motor = joint.motor;
		motor.motorSpeed = Mathf.Lerp(motor.motorSpeed, targetSpeed, Mathf.Clamp(1 * Time.deltaTime, 0, 1));
		joint.motor = motor;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Player")down(false);
	}

	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			if(gameObject.name == "Bucket Left") {
				up(false);
			}
			else {
				down(false);
			}
		}
	}

	public void up(bool external) {
		toggle(Mathf.Abs(targetSpeed));
		if(!external) sibling.down(true);
	}

	public void down(bool external) {
		toggle(-Mathf.Abs(targetSpeed));
		if(!external) sibling.up(true);
	}

	void toggle(float speed) {
		targetSpeed = speed;
	}

}
