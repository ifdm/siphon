using UnityEngine;
using System.Collections;

public class InteractingState : PlayerState {

	public GameObject target;
	private float animationEase = .4f;

	public override void HandleInput(PlayerControl player) {
		if(Input.GetButtonUp("Action") || !player.isInteracting()) {
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
		Interactable interactable = target.GetComponent<Interactable>();

		if(interactable == null){player.ChangeState(PlayerState.Idling);}

		if(animationEase > 0) {
			player.animator.TimeScale = 1;
			animationEase -= Time.deltaTime;
		}
		else {
			player.animator.Set("Push", true);
			player.animator.TimeScale = Mathf.Abs(Input.GetAxis("Horizontal"));
		}
		
		player.physics.Interact(target);

		if(interactable.offsetX >= 0) {
			float x = target.transform.position.x;
			x += interactable.offsetX * Mathf.Sign(player.transform.position.x - x);
			x = Mathf.Lerp(player.transform.position.x, x, 10 * Time.deltaTime);
			player.transform.position = new Vector3(x, player.transform.position.y, player.transform.position.z);
		}
	}

	public override void Enter(PlayerControl player, PlayerState from){
		Interactable interactable = target.GetComponent<Interactable>();
		target.rigidbody2D.mass = interactable.dynamicWeight;
		player.animator.One("Push");
		animationEase = .4f;
		interactable.moved = true;
		interactable.audio.Play();
	}

	public override void Exit(PlayerControl player, PlayerState to){
		Interactable interactable = target.GetComponent<Interactable>();

		if(interactable == null){return;}

		interactable.rigidbody2D.mass = interactable.staticWeight;
		interactable.moved = false;
		interactable.pushing = interactable.pulling = false;
		interactable.audio.Stop();
		player.animator.Set("Idle");
	}

}