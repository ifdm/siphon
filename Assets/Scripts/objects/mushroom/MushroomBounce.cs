using UnityEngine;
using System.Collections;

public class MushroomBounce : MonoBehaviour {

	public float bounceForce = 20f;
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
		Bounceable bounceable = col.gameObject.GetComponent<Bounceable>();

		if(bounceable && col.gameObject.rigidbody2D && bounceTimer == 0) {
			bounceForce = (bounceable.bounceForce != 0) ? bounceable.bounceForce : bounceForce;
			var component = col.gameObject.rigidbody2D.velocity.x + Input.GetAxis("Horizontal") * 200;

			if(Mathf.Abs(col.gameObject.rigidbody2D.velocity.y) < bounceForce) {
				col.gameObject.rigidbody2D.velocity = new Vector2(component, bounceForce);
			}
			else {
				col.gameObject.rigidbody2D.velocity = new Vector2(component, -col.gameObject.rigidbody2D.velocity.y);
			}

			col.gameObject.SendMessage("Bounced");
			bounceTimer = 0.1f;
		}
	}
}