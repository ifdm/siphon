using UnityEngine;
using System.Collections;

public class IdlingState : PlayerState {
	private bool edging = false;

	public override void HandleInput(PlayerControl player) {
		if(player.isRunning()) {
			player.ChangeState(PlayerState.Running);
		}
		else if(!player.isGrounded() || Input.GetButtonDown("Jump") && !player.physics.disableControl) {
			player.ChangeState(PlayerState.Jumping);
		}
		else if(player.getLadder() && Input.GetAxisRaw("Vertical") != 0) {
			player.ChangeState(PlayerState.Climbing);
		}

		GameObject interactable;
		if(interactable = player.isInteracting()) {
			PlayerState.Interacting.interactable = interactable;
			player.ChangeState(PlayerState.Interacting);
		}

		if(player.isIdle() && player.isUnbalanced() && player.isGrounded() && !edging) {
			Debug.Log("Playing edge.");
			player.animator.Set("Edge", true);
			edging = true;
		}
		
		player.physics.AlignSlope();
	}
	
	public override void Update(PlayerControl player) {
		player.physics.LedgeCompensate();
	}

	public override void Enter(PlayerControl player, PlayerState from) {
		player.animator.Add("Idle", true);
	}

	public override void Exit(PlayerControl player, PlayerState to) {
		edging = false;
	}
}