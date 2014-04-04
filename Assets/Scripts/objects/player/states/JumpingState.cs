using UnityEngine;
using System.Collections;

public class JumpingState : PlayerState {

	private float grace;

	public override void HandleInput(PlayerControl player) {
		player.rigidbody2D.isKinematic = false;
		player.physics.AlignUpright();

		grace -= Mathf.Min(grace, Time.deltaTime);
		if(grace > 0){return;}
		if(player.isIdle()) {
			player.ChangeState(PlayerState.Idling);
		}
		else if(player.isRunning()) {
			player.ChangeState(PlayerState.Running);
		}
		else if(player.rigidbody2D.velocity.y < 0) {
			player.ChangeState(PlayerState.Falling);
		}
		else if(player.getLadder() && Input.GetAxisRaw("Vertical") != 0) {
			player.ChangeState(PlayerState.Climbing);
		}
	}

	public override void Update(PlayerControl player) {
		player.physics.Move();
	}

	public override void Enter(PlayerControl player, PlayerState from) {
		if(Input.GetButtonDown("Jump")) {
			player.physics.Jump();
			grace = 0;
		}
		else {
			grace = .4f;
		}

		if(from == PlayerState.Running || from == PlayerState.Idling) {
			player.mozart.One("Jump");
		}

		player.animator.Set("Jump", false, 0, .2f);
	}
}