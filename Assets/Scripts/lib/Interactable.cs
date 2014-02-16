using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Interactable : MonoBehaviour {

	public bool pull = true;
	public bool push = true;

	private const float staticWeight = 100000;
	private const float dynamicWeight = 5;
	private bool inRange = false;
	private Vector2 velocity;
	private PlayerControl player;

	void Start() {
		player = GameObject.Find("Player").GetComponent<PlayerControl>();

		if(rigidbody2D == null) {
			throw new Exception("Interactable requires a rigid body.");
		}
		else {
			rigidbody2D.mass = staticWeight;
		}
	}

	void Update() {
		if(inRange && Input.GetButtonUp("Action")) {
			velocity = Vector2.zero;
		}

		if(canInteract() && velocity != Vector2.zero && inRange) {
			player.rigidbody2D.velocity = velocity;
			rigidbody2D.velocity = velocity;
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			inRange = true;

			if(canInteract()) {
				// Are we just starting to interact?
				if(player.state != PlayerState.Interacting) {
					// If so, make the interactable item movable.
					rigidbody2D.mass = dynamicWeight;
					// Change state.
					player.ChangeState(PlayerState.Interacting);
				}

				// If we start moving, build up velocity
				if(player.isRunning()) {
					var sign = Mathf.Sign(Input.GetAxis("Horizontal"));
					velocity = new Vector2(sign * 3, 0);
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			inRange = false;
			velocity = Vector2.zero;
			rigidbody2D.mass = staticWeight;
		}
	}

	private bool canInteract() {
		return player.isGrounded() && Input.GetButton("Action");
	}
}