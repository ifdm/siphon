using UnityEngine;
using System.Collections;

public class LedgingState : PlayerState {

	public override void HandleInput(GameObject player) {
		PlayerMovement movement = player.GetComponent<PlayerMovement>();
		if(Input.GetButton("Jump")) {
			movement.ChangeState(PlayerState.Jumping);
		}
	}

	public override void Enter(GameObject player) {
		player.rigidbody2D.isKinematic = true;
	}

	public override void Exit(GameObject player) {
		player.rigidbody2D.isKinematic = false;
	}
}