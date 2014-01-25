using UnityEngine;
using System.Collections;

public class MushroomBounce : MonoBehaviour {

	public float firstBounce = 20f;
	public float secondBounce = 24f;

	public float horizontalForce = 0;

	private float bounceTimer = 0;

	void Awake() {		
		RaycastHit2D hit = Physics2D.Linecast(transform.position, transform.position - (Vector3.up * 0.5f), 1 << LayerMask.NameToLayer("Ground"));
		if(hit) {
			transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
		}
		else {
			Destroy(gameObject);
		}
	}

	void Update() {
		if(bounceTimer > 0) {
			bounceTimer -= Mathf.Min(Time.deltaTime, bounceTimer);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.rigidbody2D && bounceTimer == 0) {
			float bounce;
			PlayerControl control = col.gameObject.GetComponent<PlayerControl>();
			Bounceable script = col.gameObject.GetComponent<Bounceable>();

			if(script) {
				if(script.bounced){bounce = secondBounce;}
				else{bounce = firstBounce;}
			}
			else {
				bounce = firstBounce;
			}

			col.gameObject.rigidbody2D.AddForce(new Vector2(horizontalForce, 0));

			if(Mathf.Abs(col.gameObject.rigidbody2D.velocity.y) < bounce) {
				col.gameObject.rigidbody2D.velocity = new Vector2(0, bounce);
			}
			else {
				col.gameObject.rigidbody2D.velocity = new Vector2(0, -col.gameObject.rigidbody2D.velocity.y);
			}

			// Switch to Jump State
			control.ChangeState(PlayerState.Jumping);

			col.gameObject.SendMessage("Bounce");
			bounceTimer = 0.1f;
		}
	}
}