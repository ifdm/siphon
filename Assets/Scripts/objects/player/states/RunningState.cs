using UnityEngine;
using System.Collections;

public class RunningState : PlayerState {
	public override void HandleInput(GameObject player) {
		PlayerMovement movement = player.GetComponent<PlayerMovement>();
		
		if(!movement.isGrounded() || Input.GetButtonDown("Jump")) {
			movement.ChangeState(PlayerState.Jumping);
		}
		else if(movement.isIdle()) {
			movement.ChangeState(PlayerState.Idling);
		}
		
		movement.animator.Run();
	}

	public override void Update(GameObject player) {
		PlayerMovement movement = player.GetComponent<PlayerMovement>();
		movement.control.Move();
	}
}