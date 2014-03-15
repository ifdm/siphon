using UnityEngine;
using System.Collections;

public class ClimbingState : PlayerState {
	
	private GameObject ladder;

	public override void HandleInput(PlayerControl player) {
		if(Input.GetButtonDown("Jump") && Input.GetAxisRaw("Vertical") == 0) {
			player.ChangeState(PlayerState.Jumping);
		}
		else if(player.canLedgeGrab()) {
			player.ChangeState(PlayerState.Ledging);
		}
	}

	public override void Update(PlayerControl player) {
		player.physics.Climb(ladder);

		player.animator.TimeScale = Mathf.Abs(Input.GetAxis("Vertical"));
	}

	public override void Enter(PlayerControl player, PlayerState from) {
		player.rigidbody2D.isKinematic = true;
		player.animator.Set("Climb", true);

		ladder = player.getLadder();
	}

	public override void Exit(PlayerControl player, PlayerState to) {
		player.rigidbody2D.isKinematic = false;
	}
}