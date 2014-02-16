using UnityEngine;
using System.Collections;

public class InteractingState : PlayerState {

	public override void HandleInput(PlayerControl player) {
		if(Input.GetButtonUp("Action")) {
			if(!player.isGrounded() || Input.GetButtonDown("Jump")) {
				player.ChangeState(PlayerState.Jumping);
			}
			else if(player.isIdle()) {
				player.ChangeState(PlayerState.Idling);
			}
			else {
				player.ChangeState(PlayerState.Running);
			}
		}
	}
	
}