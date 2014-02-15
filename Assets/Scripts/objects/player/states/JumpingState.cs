using UnityEngine;
using System.Collections;

public class JumpingState : PlayerState {

	public override void HandleInput(PlayerControl player) {
		player.rigidbody2D.isKinematic = false;
	
		if(player.isIdle()) {
			player.ChangeState(PlayerState.Idling);
		}
		else if(player.isRunning()) {
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
		player.physics.Move();

		Transform animation = player.transform.Find("Animation");
		//animation.rotation = Quaternion.Lerp(animation.rotation, Quaternion.identity, 4 * Time.deltaTime);
	}

	public override void Enter(PlayerControl player, PlayerState from) {
		if(Input.GetButtonDown("Jump")) {
			player.physics.Jump();
		}

		if(from == PlayerState.Running || from == PlayerState.Idling) {
			player.mozart.One("Jump");
		}

		player.animator.Set("Jump", false, 0, 2f);	
	}
}