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
		ChangeState(PlayerState.Idling);

		control = GetComponent<Controllable>();
		animator = GetComponent<PlayerAnimation>();

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
		Debug.Log(state);
		this.state = state;
		this.state.Enter(gameObject);
	}
	
	public bool isGrounded() {
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

	public bool isIdle() {
		return rigidbody2D.velocity.x == 0 && isGrounded();
	}
}
