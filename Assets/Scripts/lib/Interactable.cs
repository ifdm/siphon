using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Interactable : MonoBehaviour {

	public bool enabled = true;

	private const float staticWeight = 100000;
	private const float dynamicWeight = 5;

	void Start() {
		if(rigidbody2D == null) {
			throw new Exception("Interactable requires a rigid body.");
		}
		else {
			rigidbody2D.mass = staticWeight;
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			PlayerControl player = col.gameObject.GetComponent<PlayerControl>();
			PlayerPhysics physics = col.gameObject.GetComponent<PlayerPhysics>();
			if(player.isGrounded() && Input.GetButtonDown("Action")) {
				if(player.state != PlayerState.Interacting) {
					rigidbody2D.mass = dynamicWeight;
					player.ChangeState(PlayerState.Interacting);
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		rigidbody2D.mass = staticWeight;
		if(col.gameObject.tag == "Player") {
			PlayerPhysics physics = col.gameObject.GetComponent<PlayerPhysics>();
		}
	}
}