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
		
		player.animator.Run();
	}

	public override void Update(PlayerControl player) {
		player.physics.Move();		
	}

	public override void Enter(PlayerControl player) {
		player.animator.Run();
	}
}