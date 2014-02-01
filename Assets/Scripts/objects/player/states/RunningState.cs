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
		player.animator.TimeScale = (Mathf.Abs(player.rigidbody2D.velocity.x) / 8) + .6f;
		player.physics.Move();

		if(Input.GetAxisRaw("Horizontal") == 0) {
			player.rigidbody2D.velocity = new Vector2(0, player.rigidbody2D.velocity.y);
		}
		
		/*Vector2 normal = player.normal();
		if(normal != Vector2.zero) {
			Transform animation = player.transform.Find("Animation");
			if(player.transform.lossyScale.x < 0){normal.x *= -1;}
			animation.rotation = Quaternion.Lerp(animation.rotation, Quaternion.FromToRotation(Vector3.up, (Vector3) normal), 5 * Time.deltaTime);
		}*/
	}

	public override void Enter(PlayerControl player, PlayerState from) {
		player.animator.Set("Run", true);
	}
	
	public override void Exit(PlayerControl player, PlayerState from) {
		Transform animation = player.transform.Find("Animation");
		animation.rotation = Quaternion.FromToRotation(Vector3.up, Vector3.up);
	}
}