using UnityEngine;
using System;

public class PlayerPhysics : MonoBehaviour {
	[HideInInspector] public bool facingRight = true;

	public float moveForce = 190f;
	public float maxSpeed = 5f;
	public float jumpForce = 13;
	public float lethalVelocity = 35;

	[HideInInspector] public bool airMove = true;
	[HideInInspector] public float timeSinceFall = 0;

	public void Move(float factor = 1.0f, bool allowed = true) {
		bool grounded = GetComponent<PlayerControl>().isGrounded();
		if(grounded) {
			airMove = true;
			Vector2 normal = GetComponent<PlayerControl>().normal();
			if(normal != Vector2.zero && normal.y < 1 && ((facingRight && normal.x < 0) || (!facingRight && normal.x > 0))) {
				factor = (3 * Mathf.Cos(normal.y));
			}
		}
		else {
			factor *= 0.4f;
		}

		if(airMove) {
			float h = Input.GetAxisRaw("Horizontal");

			if(h != 0) {
				rigidbody2D.AddForce(Vector2.right * h * moveForce * factor);
			}
			
			if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed) {
				rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
			}

			if((h > 0 && !facingRight) || (h < 0 && facingRight)) {
				if(allowed) ChangeDirection();
			}
		}
	}

	public void ChangeDirection() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void Climb() {
		float v = Input.GetAxis("Vertical");
		
		Vector2 p1 = (Vector2)transform.position;
		Vector2 p2 = (Vector2)transform.position;
		Vector2 scale = (Vector2)transform.lossyScale;
		BoxCollider2D box = GetComponent<BoxCollider2D>();
		CircleCollider2D circle = GetComponent<CircleCollider2D>();

		p1 += Vector2.Scale(box.center, scale);
		p2 += Vector2.Scale(box.center, scale);
		p1.x -= box.size.x * scale.x * 0.5f;
		p2.x += box.size.x * scale.x * 0.5f;

		if(Mathf.Sign(v) > 0) {
			p1.y += (box.size.y * scale.y) - .15f;
			p2.y += (box.size.y * scale.y) - .15f;;
		}
		else {
			p1.y -= (((box.center.y - circle.center.y) - ((2 * circle.radius) - (box.size.y / 2))) * scale.y);
			p2.y -= (((box.center.y - circle.center.y) - ((2 * circle.radius) - (box.size.y / 2))) * scale.y);
		}
		
		Debug.DrawLine(p1, p2, Color.green);

		if(!Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"))) {
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, (v * (maxSpeed / 2)));
		}
		else {
			rigidbody2D.velocity = Vector2.zero;
		}
	}
	
	public void Jump() {
		rigidbody2D.isKinematic = false;
		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
	}

	// Black magic that prevents you from sliding off corner of platform.
	public void LedgeCompensate() {
		Vector2 scale = (Vector2) transform.lossyScale;
		CircleCollider2D circle = GetComponent<CircleCollider2D>();
		Vector2 p1 = Vector2.Scale(circle.center, scale) + (Vector2) transform.position;
		Vector2 p2 = p1;

		p2.y -= circle.radius * scale.y + 0.07f;

		p1.x -= circle.radius * Mathf.Abs(scale.x);
		p2.x -= circle.radius * Mathf.Abs(scale.x);
		RaycastHit2D left = Physics2D.Linecast(p1, p2, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("One-Way Ground")));

		p1.x += circle.radius * Mathf.Abs(scale.x) * 2;
		p2.x += circle.radius * Mathf.Abs(scale.x) * 2;
		RaycastHit2D right = Physics2D.Linecast(p1, p2, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("One-Way Ground")));

		if(left && !right) {
			rigidbody2D.AddForce(new Vector2(-30 * Mathf.Pow(2, -(timeSinceFall + .5f)), 0));
		}

		if(right && !left) {
			rigidbody2D.AddForce(new Vector2(30 * Mathf.Pow(2, -(timeSinceFall + .5f)), 0));
		}

		timeSinceFall += Time.deltaTime;
	}
}