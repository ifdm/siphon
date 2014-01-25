using UnityEngine;
using System.Collections;

public class IdlingState : PlayerState {

	public override void HandleInput(PlayerControl player) {
		if(player.isRunning()) {
			player.ChangeState(PlayerState.Running);
		}

		if(!player.isGrounded() || Input.GetButtonDown("Jump")) {
			player.ChangeState(PlayerState.Jumping);
		}
	}

	public override void Enter(PlayerControl player, PlayerState from) {
		player.animator.Idle();
	}
}