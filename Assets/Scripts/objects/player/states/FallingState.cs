using UnityEngine;
using System.Collections;

public class FallingState : PlayerState {

	private float fallVelocity = 0;

	public override void Update(PlayerControl player) {
		player.physics.Move();

		fallVelocity = player.rigidbody2D.velocity.y == 0 ? fallVelocity : player.rigidbody2D.velocity.y;

		if(player.canLedgeGrab()) {
			player.ChangeState(PlayerState.Ledging);
		}
		else if(player.isIdle()) {
			player.ChangeState(PlayerState.Idling);
		}
		else if(player.isGrounded()) {
			player.ChangeState(PlayerState.Running);
		}
		else if(player.canClimb()) {
			player.ChangeState(PlayerState.Climbing);
		}
	}

	public override void Enter(PlayerControl player, PlayerState from) {
		player.animator.Set("Fall", true);
	}

	public override void Exit(PlayerControl player, PlayerState to) {
		player.animator.TimeScale = 2f;
		player.animator.Set("Land");
		if(fallVelocity < -player.physics.lethalVelocity && player.isGrounded()){Application.LoadLevel(Application.loadedLevel);}
	}
}