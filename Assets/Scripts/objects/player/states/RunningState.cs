using UnityEngine;
using System.Collections;

public class RunningState : PlayerState {
	public override void HandleInput(PlayerControl player) {
		if(!player.isGrounded() || Input.GetButtonDown("Jump")) {
			player.ChangeState(PlayerState.Jumping);
		}
		else if(player.isIdle()) {
			player.ChangeState(PlayerState.Idling);
		}

		if(Input.GetAxis("Horizontal") == 0) {
			player.rigidbody2D.velocity = new Vector2(0, player.rigidbody2D.velocity.y);
		}
		
		player.animator.Run();
	}

	public override void Update(PlayerControl player) {
		player.physics.Move();
	}
}