using UnityEngine;
using System.Collections;

public class RunningState : PlayerState {
	public override void HandleInput(PlayerControl player) {
		if(!player.isGrounded() && player.rigidbody2D.velocity.y < 0) {
			player.ChangeState(PlayerState.Falling);
		}
		else if(!player.isGrounded() || Input.GetButtonDown("Jump")) {
			player.ChangeState(PlayerState.Jumping);
		}
		else if(player.isSliding()) {
			player.ChangeState(PlayerState.Sliding);
		}
		else if(player.isIdle()) {
			player.ChangeState(PlayerState.Idling);
		}
		else if(player.getLadder() && Input.GetAxisRaw("Vertical") != 0) {
			player.ChangeState(PlayerState.Climbing);
		}

		
		GameObject target;
		if(target = player.isInteracting()) {
			PlayerState.Interacting.target = target;
			player.ChangeState(PlayerState.Interacting);
		}
		
		player.physics.AlignSlope();
	}

	public override void Update(PlayerControl player) {
		if(player.physics.disableControl) {
			player.ChangeState(PlayerState.Idling);
			return;
		}

		player.animator.TimeScale = (Mathf.Abs(player.rigidbody2D.velocity.x) / 8) + .3f;
		player.physics.Move();

		if(Input.GetAxisRaw("Horizontal") == 0) {
			player.rigidbody2D.velocity = new Vector2(0, player.rigidbody2D.velocity.y);
		}

		player.physics.LedgeCompensate();
	}

	public override void Enter(PlayerControl player, PlayerState from) {
		player.animator.Set("Run", true);
	}
}