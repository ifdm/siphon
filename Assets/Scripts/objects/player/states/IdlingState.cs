using UnityEngine;
using System.Collections;

public class IdlingState : PlayerState {

	public override void HandleInput(GameObject player) {
		PlayerMovement movement = player.GetComponent<PlayerMovement>();
		if(Input.GetAxis("Horizontal") != 0) {
			movement.ChangeState(PlayerState.Running);
		}
	}

	public override void Update(GameObject player) {
		PlayerMovement movement = player.GetComponent<PlayerMovement>();
		movement.animator.Idle();
		
		if(!movement.isGrounded() || Input.GetButtonDown("Jump")) {
			movement.ChangeState(PlayerState.Jumping);
		}
	}

	public override void Enter(GameObject player) {

	}
}