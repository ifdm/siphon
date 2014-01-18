using UnityEngine;
using System.Collections;

public class FallingState : PlayerState {

	public override void Update(PlayerControl player) {
		player.physics.Move(0.25f);

		if(player.canLedgeGrab()) {
			player.ChangeState(PlayerState.Ledging);
		}
		else if(player.isGrounded()) {
			player.ChangeState(PlayerState.Idling);
		}
		else if(player.canClimb()) {
			player.ChangeState(PlayerState.Climbing);
		}
	}

	public override void Enter(PlayerControl player) {
		player.animator.Fall();
	}
}