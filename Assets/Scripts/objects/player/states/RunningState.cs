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
	}

	public override void Update(PlayerControl player) {
		player.animator.TimeScale = Mathf.Abs(player.rigidbody2D.velocity.x) / 4;
		player.physics.Move();

		if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) {
			player.rigidbody2D.velocity = new Vector2(0, player.rigidbody2D.velocity.y);
		}
	}

	public override void Enter(PlayerControl player) {
		player.animator.Run();
	}
}