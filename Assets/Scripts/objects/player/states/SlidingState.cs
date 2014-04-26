using UnityEngine;
using System;
using System.Collections;

public class SlidingState : PlayerState {

	public override void HandleInput(PlayerControl player) {
		if(!player.isSliding()) {
			player.ChangeState(PlayerState.Idling);
		}
	}

	public override void Update(PlayerControl player) {
		if(Mathf.Sign(player.rigidbody2D.velocity.x) != Mathf.Sign(player.transform.lossyScale.x)) {
			player.physics.ChangeDirection();
		}
	}

	public override void Enter(PlayerControl player, PlayerState from) {
		player.animator.Set("Slide", true);
		player.physics.disableControl = true;
	}

	public override void Exit(PlayerControl player, PlayerState to) {
		player.physics.disableControl = false;
	}
}