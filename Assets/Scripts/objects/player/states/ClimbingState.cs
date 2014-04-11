using UnityEngine;
using System.Collections;

public class ClimbingState : PlayerState {
	
	private GameObject ladder;
	private float ledgeGrace = 0;

	public override void HandleInput(PlayerControl player) {
		if(Input.GetButtonDown("Jump") && Input.GetAxisRaw("Vertical") == 0) {
			player.ChangeState(PlayerState.Jumping);
		}
		else if(player.canLedgeGrab() && ledgeGrace == 0) {
			player.ChangeState(PlayerState.Ledging);
		}
		
		ledgeGrace -= Mathf.Min(ledgeGrace, Time.deltaTime);
	}

	public override void Update(PlayerControl player) {
		player.physics.Climb(ladder);
		player.animator.TimeScale = Mathf.Abs(Input.GetAxis("Vertical"));
	}

	public override void Enter(PlayerControl player, PlayerState from) {
		ledgeGrace = .4f;
		ladder = player.getLadder();
		player.transform.position = new Vector3(ladder.transform.position.x + Mathf.Sign(player.transform.lossyScale.x) * -.4f, player.transform.position.y, player.transform.position.z);
		player.animator.Set("Climb", true);
		player.rigidbody2D.gravityScale = 0;
	}

	public override void Exit(PlayerControl player, PlayerState to) {
		player.rigidbody2D.gravityScale = 1.0f;
	}
}