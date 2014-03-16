using UnityEngine;
using System.Collections;
using Spine;

public class PlayerControl : MonoBehaviour {

	[HideInInspector] public SkeletonAnimation skeletonAnimation;

	[HideInInspector] public PlayerAnimator animator;
	[HideInInspector] public PlayerAudio mozart;
	[HideInInspector] public PlayerState state;
	[HideInInspector] public PlayerPhysics physics;

	[HideInInspector] public bool climbDirty = false;

	void Start() {
		physics = GetComponent<PlayerPhysics>();
		animator = transform.Find("Animation").GetComponent<PlayerAnimator>();
		mozart = GetComponent<PlayerAudio>();

		ChangeState(PlayerState.Idling);
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.R)){Application.LoadLevel(Application.loadedLevel);}
		state.HandleInput(this);
	}

	void FixedUpdate() {
		state.Update(this);
	}

	public void ChangeState(PlayerState next) {
		PlayerState previous = this.state;

		if(previous != null && (next == previous)) return;
		if(previous != null) {previous.Exit(this, next);}

		this.state = next;
		this.state.Enter(this, previous);
	}
	
	public Vector2 normal() {
		Vector2 scale = (Vector2) transform.lossyScale;
		CircleCollider2D circle = GetComponent<CircleCollider2D>();
		Vector2 p1 = Vector2.Scale(circle.center, scale) + (Vector2) transform.position;
		Vector2 p2 = p1;

		p2.y -= circle.radius * scale.y + 0.07f;
		
		float inc = circle.radius * -scale.x;
		p1.x -= inc;
		p2.x -= inc;
		
		RaycastHit2D cast;
		cast = Physics2D.Linecast(p1, p2, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("One-Way Ground")));
		if(cast){return cast.normal;}

		p1.x += inc;
		p2.x += inc;
		cast = Physics2D.Linecast(p1, p2, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("One-Way Ground")));
		if(cast){return cast.normal;}

		p1.x += inc;
		p2.x += inc;
		cast = Physics2D.Linecast(p1, p2, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("One-Way Ground")));
		if(cast){return cast.normal;}

		return Vector2.zero;
	}

	public bool isUnbalanced() {
		Vector2 scale = (Vector2) transform.lossyScale;
		CircleCollider2D circle = GetComponent<CircleCollider2D>();
		Vector2 p1 = Vector2.Scale(circle.center, scale) + (Vector2) transform.position;
		Vector2 p2 = p1;

		p2.y -= circle.radius * scale.y + 0.07f;
		p1.x -= (circle.radius * scale.x) / 1.6f;
		p2.x -= (circle.radius * scale.x) / 1.6f;

		//Debug.DrawLine(p1, p2, Color.red);

		RaycastHit2D left = Physics2D.Linecast(p1, p2, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("One-Way Ground")));
		if(!left && GetComponent<PlayerPhysics>().timeSinceFall > 1 && normal().x == 0){return true;}

		p1.x += (circle.radius * scale.x * 2) / 1.6f;
		p2.x += (circle.radius * scale.x * 2) / 1.6f;

		//Debug.DrawLine(p1, p2, Color.red);

		RaycastHit2D right = Physics2D.Linecast(p1, p2, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("One-Way Ground")));
		if(!right && GetComponent<PlayerPhysics>().timeSinceFall > 1 && normal().x == 0){return true;}

		return false;
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
		//Debug.DrawLine(p1, p2, Color.red);
		RaycastHit2D left = Physics2D.Linecast(p1, p2, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("One-Way Ground")));
		if(left){return true;}

		p1.x += circle.radius * scale.x;
		p2.x += circle.radius * scale.x;
		//Debug.DrawLine(p1, p2, Color.red);
		RaycastHit2D center = Physics2D.Linecast(p1, p2, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("One-Way Ground")));
		if(center){return true;}

		p1.x += circle.radius * scale.x;
		p2.x += circle.radius * scale.x;
		//Debug.DrawLine(p1, p2, Color.red);
		RaycastHit2D right = Physics2D.Linecast(p1, p2, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("One-Way Ground")));
		if(right){return true;}

		return false;
	}
	
	public bool isIdle() {
		return isGrounded() && Input.GetAxisRaw("Horizontal") == 0;
	}

	public bool isRunning() {
		return !GetComponent<PlayerPhysics>().disableControl && isGrounded() && Input.GetAxisRaw("Horizontal") != 0;
	}

	public GameObject isInteracting() {
		RaycastHit2D cast;
		Vector2 p1 = (Vector2) transform.position;
		Vector2 p2 = (Vector2) transform.position;
		Vector2 scale = (Vector2) transform.lossyScale;
		BoxCollider2D box = GetComponent<BoxCollider2D>();
		p1 += Vector2.Scale(box.center, scale);
		p2 += Vector2.Scale(box.center, scale);
		p2.x += box.size.x * scale.x * 1.5f;

		Debug.DrawLine(p1, p2, Color.green);
		if(cast = Physics2D.Linecast(p1, p2, (1 << LayerMask.NameToLayer("Ground")))) {
			if(isGrounded() && Input.GetButton("Action") && cast.rigidbody && cast.rigidbody.gameObject.GetComponent<Interactable>()) {
				return cast.rigidbody.gameObject;
			}
		}

		return null;
	}

	public bool isSliding() {
		Vector2 n = normal();
		return isGrounded() && n.x > .5 && rigidbody2D.velocity.y < 0;
	}

	public bool canLedgeGrab() {
		if(rigidbody2D.velocity.y < -GetComponent<PlayerPhysics>().dangerousVelocity) {
			return false;
		} 

		Vector2 p1 = (Vector2) transform.position;
		Vector2 p2 = (Vector2) transform.position;
		Vector2 scale = (Vector2) transform.lossyScale;
		BoxCollider2D box = GetComponent<BoxCollider2D>();
		p1 += Vector2.Scale(box.center, scale);
		p2 += Vector2.Scale(box.center, scale);

		p2.y -= box.size.y * scale.y * 1.3f;
		//Debug.DrawLine(p1, p2, Color.green);
		if(Physics2D.Linecast(p1, p2, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("One-Way Ground")))) {
			return false;
		}

		p2.y = p1.y;
		p1.y += box.size.y * scale.y * .3f;
		p2.y += box.size.y * scale.y * .3f;
		p2.x += box.size.x * scale.x * 1.5f;

		//Debug.DrawLine(p1, p2, Color.blue);
		
		RaycastHit2D cast = Physics2D.Linecast(p1, p2, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("One-Way Ground")));
		if(cast && cast.collider.gameObject.tag != "NoLedgeGrab") {
			p1.y += box.size.y * scale.y * .25f;
			p2.y += box.size.y * scale.y * .25f;
			//p2 = new Vector2(cast.point.x + (.05f * scale.x), p2.y);
			if(!Physics2D.Linecast(p1, p2, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("One-Way Ground")))) {
				return true;
			}
		}
		else {
			p1.y += box.size.y * scale.y * .25f;
			p2.y += box.size.y * scale.y * .25f;
		
			//Debug.DrawLine(p1, p2, Color.blue);
		}

		return false;
	}
	
	public GameObject getLadder() {
		if(Input.GetAxisRaw("Vertical") == 0) {
			climbDirty = false;
		}

		if(climbDirty) {
			return null;
		}

		GameObject[] ladders = GameObject.FindGameObjectsWithTag("Ladder");
		foreach(GameObject obj in ladders) {
			Climbable climbable = obj.GetComponent<Climbable>();
			RaycastHit2D cast = Physics2D.Linecast(climbable.startPoint, climbable.endPoint, 1 << LayerMask.NameToLayer("Player"));
			if(cast) {
				return obj;
			}
		}

		return null;
	}
	
	public void AnimationEvent(string action) {
		transform.Find("Landing").GetComponent<ParticleSystem>().Emit(30);
	}
}
