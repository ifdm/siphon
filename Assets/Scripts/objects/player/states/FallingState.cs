using UnityEngine;
using System.Collections;

public class FallingState : PlayerState {

	public override void Update(GameObject player) {
		PlayerMovement movement = player.GetComponent<PlayerMovement>();
		movement.control.Move();

		if(movement.canLedgeGrab()) {
			movement.ChangeState(PlayerState.Ledging);
		}
		else if(movement.isGrounded()) {
			movement.ChangeState(PlayerState.Idling);
		}
	}

	public override void Enter(GameObject player) {
		PlayerMovement movement = player.GetComponent<PlayerMovement>();
		movement.animator.Fall();
	}
}