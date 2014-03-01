using UnityEngine;
using System;
using System.Collections;

public class SlidingState : PlayerState {

	public override void HandleInput(PlayerControl player) {
		if(Input.GetButtonDown("Jump")) {
			player.ChangeState(PlayerState.Jumping);
		}
		else if(!player.isSliding()) {
			player.ChangeState(PlayerState.Idling);
		}
	}

	public override void Update(PlayerControl player) {
		player.physics.Slide();
	}

	public override void Enter(PlayerControl player, PlayerState from) {
		Debug.Log("I'm Sliding!");
	}

	public override void Exit(PlayerControl player, PlayerState to) {
		Debug.Log("I'm not sliding!");
	}
}