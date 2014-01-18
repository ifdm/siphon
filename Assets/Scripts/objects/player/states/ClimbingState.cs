using UnityEngine;
using System.Collections;

public class ClimbingState : PlayerState {
	
	public override void HandleInput(GameObject player) {

	}

	public override void Update(GameObject player) {
		Controllable control = player.GetComponent<Controllable>();
		control.Climb();
	}

	public override void Enter(GameObject player) {
		player.rigidbody2D.isKinematic = true;
	}

	public override void Exit(GameObject player) {
		player.rigidbody2D.isKinematic = false;
	}
}