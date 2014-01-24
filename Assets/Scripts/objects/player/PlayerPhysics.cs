using UnityEngine;
using System;

public class PlayerPhysics : MonoBehaviour {
	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;

	public float moveForce = 190f;
	public float maxSpeed = 5f;
	public float jumpForce = 575f;

	public void Move(float factor = 1.0f) {
		float h = Input.GetAxis("Horizontal");

		if(h * rigidbody2D.velocity.x < maxSpeed) {
			rigidbody2D.AddForce(Vector2.right * h * moveForce * factor);
		}
		
		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed) {
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		}

		if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) {
			rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
		}

		if((h > 0 && !facingRight) || (h < 0 && facingRight)) {
			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
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
			p1.y += box.size.y * scale.y;
			p2.y += box.size.y * scale.y;
		}
		else {
			p1.y -= (((box.center.y - circle.center.y) - ((2 * circle.radius) - (box.size.y / 2))) * scale.y);
			p2.y -= (((box.center.y - circle.center.y) - ((2 * circle.radius) - (box.size.y / 2))) * scale.y);
		}
		
		Debug.DrawLine(p1, p2, Color.green);

		if(!Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"))) {
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, (v * maxSpeed));
		}
		else {
			rigidbody2D.velocity = Vector2.zero;
		}
	}
	
	public void Jump() {
		rigidbody2D.isKinematic = false;
		rigidbody2D.AddForce(new Vector2(0f, jumpForce));
	}
}