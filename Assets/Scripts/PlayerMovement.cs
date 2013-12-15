using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;
	[HideInInspector]
	public bool jump = false;
	
	public float moveForce = 190f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;

	private Transform feet;

	enum PlayerState {WALKING, JUMPING, LEDGING, CLIMBING};
	private PlayerState state;

	bool bounced = false;
	float bounce1 = 875f;
	float bounce2 = 975f;
	
	void MushroomBounceEvent() {
		if (bounced) {
			rigidbody2D.AddForce (new Vector2 (0, bounce2));
		}
		else {
			rigidbody2D.AddForce(new Vector2(0, bounce1));
			bounced = true;
		}
	}

	void Awake() {
		state = PlayerState.WALKING;
		feet = transform.Find("Player Feet");
	}
	
	void Update() {
		switch(state) {
			case PlayerState.WALKING:
				rigidbody2D.isKinematic = false;
				bounced = false;
				
				if(!Physics2D.Linecast(transform.position, feet.position, 1 << LayerMask.NameToLayer("Ground"))) {
					state = PlayerState.JUMPING;
				}
			break;

			case PlayerState.JUMPING:
				rigidbody2D.isKinematic = false;
			
				if(Physics2D.Linecast(transform.position, feet.position, 1 << LayerMask.NameToLayer("Ground"))) {
					state = PlayerState.WALKING;
				}
				
				else if(rigidbody2D.velocity.y < 0) {
					Vector3 eye1 = transform.position;
					Vector3 eye2 = transform.position;
					eye1.y += 0.5f;
					eye2.y += 0.5f;
					if(facingRight){eye2.x += 0.7f;}
					else{eye2.x -= 0.7f;}
					
					Debug.DrawLine(eye1, eye2, Color.blue);
					
					if(Physics2D.Linecast(eye1, eye2, 1 << LayerMask.NameToLayer("Ground"))) {
						state = PlayerState.LEDGING;
					}
				}
			break;

			case PlayerState.LEDGING:
				rigidbody2D.isKinematic = true;
			break;
			
			case PlayerState.CLIMBING:
				rigidbody2D.isKinematic = true;
			break;
		}
	}	
	
	void FixedUpdate() {
		switch(state) {
			case PlayerState.WALKING:
				Move();
				Jump();
			break;
			
			case PlayerState.JUMPING:
				Move();
			break;
			
			case PlayerState.LEDGING:
				Jump();
			break;
			
			case PlayerState.CLIMBING:
				Climb();
				Jump();
			break;
		}
	}
	
	void Move() {
		float h = Input.GetAxis("Horizontal");
				
		if(h * rigidbody2D.velocity.x < maxSpeed) {
			rigidbody2D.AddForce(Vector2.right * h * moveForce);
		}
		
		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed) {
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		}
		
		if((h > 0 && !facingRight) || (h < 0 && facingRight)) {
			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}
	
	void Climb() {
		//
	}
	
	void Jump() {
		if(Input.GetButtonDown("Jump")) {
			rigidbody2D.isKinematic = false;
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			state = PlayerState.JUMPING;
		}
	}
}
