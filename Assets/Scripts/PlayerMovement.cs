using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	
	public float moveForce = 190f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;

	public enum PlayerState {WALKING, JUMPING, LEDGING, CLIMBING};
	public PlayerState state;

	private bool bounced = false;
	private float bounce1 = 875f;
	private float bounce2 = 975f;
	
	private float canLedge = 0;
	
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
	}
	
	void Update() {
		switch(state) {
			case PlayerState.WALKING:
				rigidbody2D.isKinematic = false;
				bounced = false;
				
				if(!isGrounded()) {
					state = PlayerState.JUMPING;
				}
				
				if(Input.GetButtonDown("Jump")){jump = true;}
			break;

			case PlayerState.JUMPING:
				rigidbody2D.isKinematic = false;
			
				if(isGrounded()) {
					state = PlayerState.WALKING;
				}
				
				else if(rigidbody2D.velocity.y < 0 && canLedge == 0) {
					Vector3 eye1 = transform.position;
					Vector3 eye2 = transform.position;
					Vector3 extents = GetComponent<BoxCollider2D>().size * .5f;
					extents.x *= transform.lossyScale.x;
					extents.y *= transform.lossyScale.y;
					eye1.y += (extents.y * .75f);
					eye2.y += (extents.y * .75f);
					eye2.x += extents.x;
					if(facingRight){eye2.x += .08f;}
					else{eye2.x -= .08f;}
					
					Debug.DrawLine(eye1, eye2, Color.blue);
					
					if(Physics2D.Linecast(eye1, eye2, 1 << LayerMask.NameToLayer("Ground"))) {
						eye1.y += 0.1f;
						eye2.y += 0.1f;
						if(!Physics2D.Linecast(eye1, eye2, 1 << LayerMask.NameToLayer("Ground"))) {
							state = PlayerState.LEDGING;
						}
					}
					else {
						eye1.y += 0.2f;
						eye2.y += 0.2f;
					
						Debug.DrawLine(eye1, eye2, Color.blue);
					}
				}
			break;

			case PlayerState.LEDGING:
				rigidbody2D.isKinematic = true;
				if(Input.GetButtonDown("Jump")){jump = true;}
			break;
			
			case PlayerState.CLIMBING:
				rigidbody2D.isKinematic = true;
				if(Input.GetButtonDown("Jump")){jump = true;}
			break;
		}
		
		if(canLedge > 0){canLedge -= Mathf.Min(Time.deltaTime, canLedge);}
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
				Ledge();
			break;
			
			case PlayerState.CLIMBING:
				Climb();
				Jump();
			break;
		}
	}
	
	void Move() {
		float h = Input.GetAxis("Horizontal");
		
		if(Input.GetKeyDown(KeyCode.A)){h = -1;}
		else if(Input.GetKeyDown(KeyCode.D)){h = 1;}
		
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
	
	void Ledge() {
		canLedge = .1f;
		Jump();
		
		if(Input.GetAxis("Vertical") < 0) {
			state = PlayerState.JUMPING;
		}	
	}
	
	void Climb() {
		float v = Input.GetAxis("Vertical");
		
		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, (v * maxSpeed));
	}
	
	void Jump() {
		if(jump) {
			rigidbody2D.isKinematic = false;
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			state = PlayerState.JUMPING;
			jump = false;
		}
	}
	
	bool isGrounded() {
		// Can probably cache most of this (sans raycasts)
		Vector3 p1 = transform.position;
		Vector3 p2 = transform.position;
		Vector3 extents = GetComponent<BoxCollider2D>().size * .5f;
		extents.y += GetComponent<CircleCollider2D>().radius * .5f;
		extents.x *= transform.lossyScale.x;
		extents.y *= transform.lossyScale.y;
		p1.x -= extents.x;
		p2.x -= extents.x;
		p2.y -= extents.y;
		Debug.DrawLine(p1, p2, Color.red);
		bool left = Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"));
		p1.x += (2 * extents.x);
		p2.x += (2 * extents.x);
		Debug.DrawLine(p1, p2, Color.red);
		bool right = Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"));
		p1.x -= (extents.x);
		p2.x -= (extents.x);
		Debug.DrawLine(p1, p2, Color.red);
		bool center = Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"));
		return left || right || center;
	}
}
