using UnityEngine;
using System.Collections;

public class MushroomBounce : MonoBehaviour {

	public float bounceForce = 20f;
	public float horizontalForce = 0;

	private float bounceTimer = 0;
	private MushroomAnimator animator;

	void Awake() {		
		RaycastHit2D hit = Physics2D.Linecast(transform.position, transform.position - (Vector3.up * 0.5f), 1 << LayerMask.NameToLayer("Ground"));
		if(hit) {
			transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
		}
		else {
			Destroy(gameObject);
		}
	}

	void Start() {
		animator = GetComponent<MushroomAnimator>();
	}

	void Update() {
		if(bounceTimer > 0) {
			bounceTimer -= Mathf.Min(Time.deltaTime, bounceTimer);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		Bounceable bounceable = col.gameObject.GetComponent<Bounceable>();

		if(bounceable && col.gameObject.rigidbody2D && bounceTimer == 0) {
			bounceForce = (bounceable.bounceForce != 0) ? bounceable.bounceForce : bounceForce;
			float component = col.gameObject.rigidbody2D.velocity.x;

			if(horizontalForce > 0) {
				col.rigidbody2D.AddForce(new Vector2(horizontalForce, 0));
				component = 0;
			}
			
			if(Mathf.Abs(col.gameObject.rigidbody2D.velocity.y) < bounceForce) {
				col.gameObject.rigidbody2D.velocity = new Vector2(component, bounceForce);
			}
			else {
				col.gameObject.rigidbody2D.velocity = new Vector2(component, -col.gameObject.rigidbody2D.velocity.y);
			}

			animator.Set("Bounce");
			
			col.gameObject.SendMessage("Bounced", this);
			bounceTimer = 0.1f;
		}
	}
}