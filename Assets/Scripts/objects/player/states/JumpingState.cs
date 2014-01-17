using UnityEngine;
using System.Collections;

public class JumpingState : PlayerState {

	public override void HandleInput(GameObject player) {
		PlayerMovement movement = player.GetComponent<PlayerMovement>();

		player.rigidbody2D.isKinematic = false;
	
		if(movement.isIdle()) {
			movement.ChangeState(PlayerState.Idling);
		}
		else if(movement.isGrounded()) {
			movement.ChangeState(PlayerState.Running);
		}
		else if(player.rigidbody2D.velocity.y < 0) {				
			Vector2 p1 = (Vector2)player.transform.position;
			Vector2 p2 = (Vector2)player.transform.position;
			Vector2 scale = (Vector2)player.transform.lossyScale;
			BoxCollider2D box = player.GetComponent<BoxCollider2D>();
			p1 += Vector2.Scale(box.center, scale);
			p2 += Vector2.Scale(box.center, scale);
			p1.y += box.size.y * scale.y * .3f;
			p2.y += box.size.y * scale.y * .3f;
			p2.x += box.size.x * scale.x * .8f;

			Debug.DrawLine(p1, p2, Color.blue);
			
			if(Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"))) {
				p1.y += box.size.y * scale.y * .25f;
				p2.y += box.size.y * scale.y * .25f;
				if(!Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"))) {
					movement.ChangeState(PlayerState.Ledging);
				}
			}
			else {
				p1.y += box.size.y * scale.y * .25f;
				p2.y += box.size.y * scale.y * .25f;
			
				Debug.DrawLine(p1, p2, Color.blue);
			}
		}
	}

	public override void Update(GameObject player) {
		PlayerMovement movement = player.GetComponent<PlayerMovement>();
		movement.control.Move();
	}

	public override void Enter(GameObject player) {
		if(Input.GetButtonDown("Jump")) {
			PlayerMovement movement = player.GetComponent<PlayerMovement>();
			player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 0.1f);

			movement.control.Jump();
			movement.animator.Jump();
		}
	}
}