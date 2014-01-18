using UnityEngine;
using System.Collections;

public class JumpingState : PlayerState {

	public override void HandleInput(PlayerControl player) {
		player.rigidbody2D.isKinematic = false;
	
		if(player.isIdle()) {
			player.ChangeState(PlayerState.Idling);
		}
		else if(player.isGrounded()) {
			player.ChangeState(PlayerState.Running);
		}
		else if(player.rigidbody2D.velocity.y < 0) {
			player.ChangeState(PlayerState.Falling);
		}
		else if(player.canClimb()) {
			player.ChangeState(PlayerState.Climbing);
		}
	}

	public override void Update(PlayerControl player) {
		player.physics.Move(.35f);
	}

	public override void Enter(PlayerControl player) {
		if(Input.GetButtonDown("Jump")) {
			player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 0.1f);

			player.physics.Jump();
			player.animator.Jump();
		}
	}
}