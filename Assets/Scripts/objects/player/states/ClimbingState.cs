using UnityEngine;
using System.Collections;

public class ClimbingState : PlayerState {
	
	public override void HandleInput(PlayerControl player) {
		if(Input.GetButtonDown("Jump")) {
			player.ChangeState(PlayerState.Jumping);
		}
	}

	public override void Update(PlayerControl player) {
		player.physics.Climb();
		if(!player.canClimb()) {
			player.ChangeState(PlayerState.Falling);
		}

		player.animator.TimeScale = Input.GetAxis("Vertical");
	}

	public override void Enter(PlayerControl player, PlayerState from) {
		player.rigidbody2D.isKinematic = true;
		player.animator.Set("Climb", true);
	}

	public override void Exit(PlayerControl player, PlayerState to) {
		player.rigidbody2D.isKinematic = false;
	}
}