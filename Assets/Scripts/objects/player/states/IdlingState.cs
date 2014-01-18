using UnityEngine;
using System.Collections;

public class IdlingState : PlayerState {

	public override void HandleInput(PlayerControl player) {
		if(Input.GetAxis("Horizontal") != 0) {
			player.ChangeState(PlayerState.Running);
		}
	}

	public override void Update(PlayerControl player) {
		player.animator.Idle();
		
		if(!player.isGrounded() || Input.GetButtonDown("Jump")) {
			player.ChangeState(PlayerState.Jumping);
		}
	}

	public override void Enter(PlayerControl player) {

	}
}