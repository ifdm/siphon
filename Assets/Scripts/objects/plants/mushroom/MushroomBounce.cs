using UnityEngine;
using System.Collections;

public class MushroomBounce : Plant {

	public float bounceForce = 20f;
	public float horizontalForce = 0;

	private float bounceTimer = 0;
	private MushroomAnimator animator;

	void Start() {
		animator = GetComponent<MushroomAnimator>();
	}

	void Update() {
		if(bounceTimer > 0) {
			bounceTimer -= Mathf.Min(Time.deltaTime, bounceTimer);
		}

		BoxCollider2D box = GetComponent<BoxCollider2D>();
		if(GameObject.Find("Player").GetComponent<PlayerControl>().isGrounded()) {
			box.size = new Vector2(150, box.size.y);
		}
		else {
			box.size = new Vector2(500, box.size.y);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		Bounceable bounceable = col.gameObject.GetComponent<Bounceable>();

		if(bounceable && col.gameObject.rigidbody2D && bounceTimer == 0) {
			bounceForce = (bounceable.bounceForce != 0) ? bounceable.bounceForce : bounceForce;
			float component = col.gameObject.rigidbody2D.velocity.x;
			
			if(Mathf.Abs(col.gameObject.rigidbody2D.velocity.y) < bounceForce) {
				col.gameObject.rigidbody2D.velocity = new Vector2(component, bounceForce);
			}
			else {
				col.gameObject.rigidbody2D.velocity = new Vector2(component, -col.gameObject.rigidbody2D.velocity.y);
			}

			if(animator){animator.Set("Bounce");}
			
			col.gameObject.SendMessage("Bounced", false);
			bounceTimer = 0.1f;
		}
	}
		
	public override bool canPlant(RaycastHit2D cast) {
		if(!cast){return false;}
		
		Vector3 p1 = cast.point;
		Vector3 p2 = cast.point;
		
		p1.y -= .01f;
		p2.y -= .01f;
		
		p2.y -= .1f;
		
		p1.x -= .1f;
		p2.x -= .1f;
		Debug.DrawLine(p1, p2, Color.blue);
		if(!Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"))) {
			return false;
		}
		
		p1.x += .1f;
		p2.x += .1f;
		Debug.DrawLine(p1, p2, Color.blue);
		if(!Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"))) {
			return false;
		}
		
		p1.x += .1f;
		p2.x += .1f;
		Debug.DrawLine(p1, p2, Color.blue);
		if(!Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"))) {
			return false;
		}
		
		return true;
	}
}