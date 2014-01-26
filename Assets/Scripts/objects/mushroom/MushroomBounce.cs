using UnityEngine;
using System.Collections;

public class MushroomBounce : MonoBehaviour {

	public float bounceForce = 20f;
	public float horizontalForce = 0;

	private float bounceTimer = 0;
	private MushroomAnimator animator;

	void Start() {
		animator = GetComponent<MushroomAnimator>();

		RaycastHit2D hit = Physics2D.Linecast(transform.position, transform.position - (Vector3.up * .5f), 1 << LayerMask.NameToLayer("Ground"));
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

		BoxCollider2D box = GetComponent<BoxCollider2D>();
		if(GameObject.Find("Player").GetComponent<PlayerControl>().isGrounded()) {
			box.size = new Vector2(150, box.size.y);
		}
		else {
			box.size = new Vector2(500, box.size.y);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		Bounceable bounceable = col.gameObject.GetComponent<Bounceable>();

		if(bounceable && col.gameObject.rigidbody2D && bounceTimer == 0) {
			bounceForce = (bounceable.bounceForce != 0) ? bounceable.bounceForce : bounceForce;
			float component = col.gameObject.rigidbody2D.velocity.x;
			
			if(Mathf.Abs(col.gameObject.rigidbody2D.velocity.y) < bounceForce) {
				col.gameObject.rigidbody2D.velocity = new Vector2(component, bounceForce);
			}
			else {
				col.gameObject.rigidbody2D.velocity = new Vector2(component, -col.gameObject.rigidbody2D.velocity.y);
			}

			if(animator){animator.Set("Bounce");}
			
			col.gameObject.SendMessage("Bounced", false);
			bounceTimer = 0.1f;
		}
	}
}