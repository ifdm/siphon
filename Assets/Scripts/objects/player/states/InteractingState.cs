using UnityEngine;
using System.Collections;

public class InteractingState : PlayerState {

	public GameObject interactable;

	public override void HandleInput(PlayerControl player) {
		if(Input.GetButtonUp("Action")) {
			if(!player.isGrounded() || Input.GetButtonDown("Jump")) {
				player.ChangeState(PlayerState.Jumping);
			}
			else if(player.isIdle()) {
				player.ChangeState(PlayerState.Idling);
			}
			else {
				player.ChangeState(PlayerState.Running);
			}
		}
	}

	public override void Update(PlayerControl player) {
		player.physics.Interact(interactable);
	}

	public override void Enter(PlayerControl player, PlayerState from){
		interactable.rigidbody2D.mass = interactable.GetComponent<Interactable>().dynamicWeight;
	}

	public override void Exit(PlayerControl player, PlayerState to){
		interactable.rigidbody2D.mass = interactable.GetComponent<Interactable>().staticWeight;
		interactable.GetComponent<Interactable>().pushing = interactable.GetComponent<Interactable>().pulling = false;
	}
	
}