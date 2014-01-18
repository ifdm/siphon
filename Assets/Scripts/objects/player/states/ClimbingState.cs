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
	}

	public override void Enter(PlayerControl player) {
		player.rigidbody2D.isKinematic = true;
	}

	public override void Exit(PlayerControl player) {
		player.rigidbody2D.isKinematic = false;
	}
}