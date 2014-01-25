using UnityEngine;
using System.Collections;

public class IdlingState : PlayerState {

	public override void HandleInput(PlayerControl player) {
		if(Input.GetAxis("Horizontal") != 0) {
			player.ChangeState(PlayerState.Running);
		}

		if(!player.isGrounded() || Input.GetButtonDown("Jump")) {
			player.ChangeState(PlayerState.Jumping);
		}
	}

	public override void Update(PlayerControl player) {

	}

	public override void Enter(PlayerControl player) {
		player.animator.Idle();
	}
}