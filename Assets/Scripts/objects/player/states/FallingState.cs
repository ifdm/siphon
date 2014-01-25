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
		else if(player.isGrounded()) {
			player.ChangeState(PlayerState.Idling);
		}
		else if(player.canClimb()) {
			player.ChangeState(PlayerState.Climbing);
		}
	}

	public override void Enter(PlayerControl player) {
		player.animator.Fall();
	}

	public override void Exit(PlayerControl player) {
		player.animator.Landing();
		Debug.Log(fallVelocity);
		if(fallVelocity < -50){Application.LoadLevel(Application.loadedLevel);}
	}
}