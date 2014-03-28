using UnityEngine;
using System.Collections;

public class ActiveBucket : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Player") down();
	}

	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.tag == "Player") up();
	}

	void up() {
		toggle(Mathf.Abs(gameObject.GetComponent<SliderJoint2D>().motor.motorSpeed));
	}

	void down() {
		toggle(-Mathf.Abs(gameObject.GetComponent<SliderJoint2D>().motor.motorSpeed));
	}

	void toggle(float speed) {
		var motor = gameObject.GetComponent<SliderJoint2D>().motor;
		motor.motorSpeed = speed; 

		gameObject.GetComponent<SliderJoint2D>().motor = motor;
	}

}
