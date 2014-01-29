using UnityEngine;
using System.Collections;

public class FallingState : PlayerState {

	private float fallVelocity = 0;
	private float ledgeGrace = 0;

	public override void Update(PlayerControl player) {
		player.physics.Move();

		fallVelocity = player.rigidbody2D.velocity.y == 0 ? fallVelocity : player.rigidbody2D.velocity.y;

		if(player.canLedgeGrab() && ledgeGrace == 0) {
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

		ledgeGrace -= Mathf.Min(ledgeGrace, Time.deltaTime);
	}

	public override void Enter(PlayerControl player, PlayerState from) {
		player.animator.Set("Fall", true);
		if(from == PlayerState.Ledging){ledgeGrace = 0.2f;}
	}

	public override void Exit(PlayerControl player, PlayerState to) {
		player.animator.Set("Land");
		if(fallVelocity < -player.physics.lethalVelocity && player.isGrounded()){Application.LoadLevel(Application.loadedLevel);}
	}
}