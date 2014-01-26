using UnityEngine;
using System.Collections;

public class LedgingState : PlayerState {

	public override void HandleInput(PlayerControl player) {
		if(Input.GetButton("Jump") || Input.GetAxis("Vertical") > 0) {
			player.ChangeState(PlayerState.Jumping);

			player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 0.1f);
			player.physics.Jump();
		}
		else if(Input.GetAxis("Vertical") < 0) {
			player.ChangeState(PlayerState.Falling);
		}
	}

	public override void Enter(PlayerControl player, PlayerState from) {
		player.rigidbody2D.isKinematic = true;
		player.animator.Set("Ledge");
	}

	public override void Exit(PlayerControl player, PlayerState to) {
		player.rigidbody2D.isKinematic = false;
		player.animator.Set("PullUp");
	}
}