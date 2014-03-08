using UnityEngine;
using System.Collections;

public class LedgingState : PlayerState {
	
	private float jumpTimer = 0;

	public override void HandleInput(PlayerControl player) {
		if(jumpTimer == 0) {
			if(Input.GetButtonDown("Jump") || (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Vertical") < 1)) {
				player.ChangeState(PlayerState.Jumping);

				player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 0.1f);
				player.physics.Jump();
			}
			else if(Input.GetAxis("Vertical") < 0) {
				player.ChangeState(PlayerState.Falling);
			}
		}

		/*if(!player.canLedgeGrab()) {
			player.ChangeState(PlayerState.Falling);
		}*/
		
		jumpTimer -= Mathf.Min(jumpTimer, Time.deltaTime);
		
		player.physics.AlignUpright();
	}

	public override void Enter(PlayerControl player, PlayerState from) {
		player.rigidbody2D.isKinematic = true;
		player.animator.Set("Ledge");

		Vector2 p1 = (Vector2) player.transform.position;
		Vector2 p2 = (Vector2) player.transform.position;
		Vector2 scale = (Vector2) player.transform.lossyScale;
		BoxCollider2D box = player.GetComponent<BoxCollider2D>();
		p1 += Vector2.Scale(box.center, scale);
		p2 += Vector2.Scale(box.center, scale);

		p1.y += box.size.y * scale.y * .3f;
		p2.y += box.size.y * scale.y * .3f;
		p2.x += box.size.x * scale.x * 1.5f;

		RaycastHit2D cast = Physics2D.Linecast(p1, p2, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("One-Way Ground")));
		float diff = cast.point.x - (player.transform.position.x + (box.size.x * scale.x * .75f));
		float y = player.transform.position.y;
		if(cast.rigidbody){y += Mathf.Sign(cast.rigidbody.velocity.y * .3f);}
		player.transform.position = new Vector3(player.transform.position.x + diff, y, player.transform.position.z);
		
		jumpTimer = player.physics.ledgeDuration;

		player.transform.parent = cast.transform;
	}

	public override void Exit(PlayerControl player, PlayerState to) {
		player.rigidbody2D.isKinematic = false;
		if(to == PlayerState.Jumping){player.animator.Set("PullUp");}

		player.transform.parent = null;
	}
}