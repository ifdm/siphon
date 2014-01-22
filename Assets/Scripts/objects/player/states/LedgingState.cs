using UnityEngine;
using System.Collections;

public class LedgingState : PlayerState {

	public override void HandleInput(PlayerControl player) {
		if(Input.GetButton("Jump")) {
			player.ChangeState(PlayerState.Jumping);
		}
	}

	public override void Enter(PlayerControl player) {
		player.rigidbody2D.isKinematic = true;
		player.animator.Ledging();
	}

	public override void Exit(PlayerControl player) {
		player.rigidbody2D.isKinematic = false;
		player.animator.PullingUp();
	}
}