using UnityEngine;
using System.Collections;
using Spine;

public class PlayerMovement : MonoBehaviour {
	
	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	[HideInInspector] public bool animateIdle = true;
	[HideInInspector] public bool animateJump = false;

	[HideInInspector] public SkeletonAnimation skeletonAnimation;
	
	public float moveForce = 190f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;

	public enum PlayerState {WALKING, JUMPING, LEDGING, CLIMBING, IDLING};
	public PlayerState state;
	
	private float ledgeTimer = 0;

	void Start() {
		skeletonAnimation = GetComponent<SkeletonAnimation>();
	}

	void Awake() {
		state = PlayerState.IDLING;
	}
	
	void Update() {
		switch(state) {
			case PlayerState.IDLING:
				if(!isIdle()) {
					if(!isGrounded()) {
						state = PlayerState.JUMPING;
					}
					else {
						state = PlayerState.WALKING;
					}
				}
				
				if(animateIdle) {
					skeletonAnimation.state.ClearTracks();
					skeletonAnimation.state.AddAnimation(0, "idle", false, 0);
					animateIdle = false;
				}

				if(Input.GetButtonDown("Jump")){
					jump = true;
					animateJump = true;
				}
			break;

			case PlayerState.WALKING:
				rigidbody2D.isKinematic = false;
				
				if(!isGrounded()) {
					state = PlayerState.JUMPING;
				}
				else if(isIdle()) {
					state = PlayerState.IDLING;
					animateIdle = true;
				}
		        
		        skeletonAnimation.state.AddAnimation(0, "run", false, 0);
				
				if(Input.GetButtonDown("Jump")){
					jump = true;
					animateJump = true;
				}
			break;

			case PlayerState.JUMPING:
				rigidbody2D.isKinematic = false;
			
				if(isGrounded()) {
					state = PlayerState.WALKING;
				}
				else if(isIdle()) {
					state = PlayerState.IDLING;
					animateIdle = true;
				}
				else if(rigidbody2D.velocity.y < 0 && ledgeTimer == 0) {
					Vector2 p1 = (Vector2)transform.position;
					Vector2 p2 = (Vector2)transform.position;
					Vector2 scale = (Vector2)transform.lossyScale;
					BoxCollider2D box = GetComponent<BoxCollider2D>();
					p1 += Vector2.Scale(box.center, scale);
					p2 += Vector2.Scale(box.center, scale);
					p1.y += box.size.y * scale.y * .3f;
					p2.y += box.size.y * scale.y * .3f;
					p2.x += box.size.x * scale.x * .55f;

					Debug.DrawLine(p1, p2, Color.blue);
					
					if(Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"))) {
						p1.y += box.size.y * scale.y * .1f;
						p2.y += box.size.y * scale.y * .1f;
						if(!Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"))) {
							state = PlayerState.LEDGING;
						}
					}
					else {
						p1.y += box.size.y * scale.y * .1f;
						p2.y += box.size.y * scale.y * .1f;
					
						Debug.DrawLine(p1, p2, Color.blue);
					}
				}

				if(animateJump) {
					skeletonAnimation.state.ClearTracks();
					skeletonAnimation.state.AddAnimation(1, "jump", false, 0);
					animateJump = false;
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
		
		if(ledgeTimer > 0){ledgeTimer -= Mathf.Min(Time.deltaTime, ledgeTimer);}
	}	
	
	void FixedUpdate() {
		switch(state) {
			case PlayerState.IDLING:
				Idle();
				Move();
				Jump();
			break;

			case PlayerState.WALKING:
				Idle();
				Move();
				Jump();
			break;
			
			case PlayerState.JUMPING:
				Idle();
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

	void Idle() {
		// For future physics.
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
		ledgeTimer = .1f;
		Jump();
		
		if(Input.GetAxis("Vertical") < 0) {
			state = PlayerState.JUMPING;
		}	
	}
	
	void Climb() {
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
			p1.y += box.size.y * scale.y * 0.5f;
			p2.y += box.size.y * scale.y * 0.5f;
		}
		else {
			p1.y -= (((box.center.y - circle.center.y) - ((2 * circle.radius) - (box.size.y / 2))) * scale.y);
			p2.y -= (((box.center.y - circle.center.y) - ((2 * circle.radius) - (box.size.y / 2))) * scale.y);
		}

		if(!Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"))) {
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, (v * maxSpeed));
		}
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
		Vector2 p1 = (Vector2)transform.position;
		Vector2 p2 = (Vector2)transform.position;
		Vector2 scale = (Vector2)transform.lossyScale;
		BoxCollider2D box = GetComponent<BoxCollider2D>();
		CircleCollider2D circle = GetComponent<CircleCollider2D>();
		p1 += Vector2.Scale(box.center, scale) - Vector2.Scale(box.size, scale / 2);
		p2 += Vector2.Scale(box.center, scale) - Vector2.Scale(box.size, scale / 2);
		p2.y -= (((box.center.y - circle.center.y) - ((2 * circle.radius) - (box.size.y / 2))) * scale.y);
		p1.x += box.size.x * scale.x * 0.1f;
		p2.x += box.size.x * scale.x * 0.1f;
		Debug.DrawLine(p1, p2, Color.red);
		bool left = Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"));
		p1.x += box.size.x * scale.x * 0.4f;
		p2.x += box.size.x * scale.x * 0.4f;
		Debug.DrawLine(p1, p2, Color.red);
		bool center = Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"));
		p1.x += box.size.x * scale.x * 0.4f;
		p2.x += box.size.x * scale.x * 0.4f;
		Debug.DrawLine(p1, p2, Color.red);
		bool right = Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"));
		return left || center || right;
	}

	bool isIdle() {
		return rigidbody2D.velocity.x == 0 && isGrounded();
	}
}
