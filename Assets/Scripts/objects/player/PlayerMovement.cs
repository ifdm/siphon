using UnityEngine;
using System.Collections;
using Spine;

public class PlayerMovement : MonoBehaviour {

	[HideInInspector] public static Vector3 checkpoint;
	[HideInInspector] public SkeletonAnimation skeletonAnimation;

	public PlayerAnimation animator;
	public PlayerState state;
	public Controllable control;


	void Start() {
		control = GetComponent<Controllable>();
		animator = GetComponent<PlayerAnimation>();

		ChangeState(PlayerState.Idling);

		if(PlayerMovement.checkpoint != Vector3.zero) {
			transform.position = PlayerMovement.checkpoint;
		}
	}

	void Update() {
		state.HandleInput(gameObject);
	}

	void FixedUpdate() {
		state.Update(gameObject);
	}

	public void ChangeState(PlayerState state) {
		if(this.state != null) {
			this.state.Exit(gameObject);
		}

		this.state = state;
		Debug.Log(this.state);
		this.state.Enter(gameObject);
	}
	
	public bool isGrounded() {
		// Can probably cache most of this (sans raycasts)
		Vector2 scale = (Vector2) transform.lossyScale;
		CircleCollider2D circle = GetComponent<CircleCollider2D>();
		Vector2 p1 = Vector2.Scale(circle.center, scale) + (Vector2) transform.position;
		Vector2 p2 = p1;

		p2.y -= circle.radius * scale.y + 0.07f;

		p1.x -= circle.radius * scale.x;
		p2.x -= circle.radius * scale.x;
		Debug.DrawLine(p1, p2, Color.red);
		bool left = Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"));

		p1.x += circle.radius * scale.x;
		p2.x += circle.radius * scale.x;
		Debug.DrawLine(p1, p2, Color.red);
		bool center = Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"));

		p1.x += circle.radius * scale.x;
		p2.x += circle.radius * scale.x;
		Debug.DrawLine(p1, p2, Color.red);
		bool right = Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"));

		return left || center || right;
	}

	public bool isIdle() {
		return rigidbody2D.velocity.x == 0 && isGrounded() && Input.GetAxis("Horizontal") == 0;
	}
}
