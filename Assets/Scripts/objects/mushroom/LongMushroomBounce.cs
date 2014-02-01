using UnityEngine;
using System.Collections;

public class LongMushroomBounce : MonoBehaviour {

	public float bounceForce = 20f;
	public float horizontalForce = 0;

	private float bounceTimer = 0;

	private Vector3 p1;
	private Vector3 p2;

	void Start() {
		BoxCollider2D box = GetComponent<BoxCollider2D>();
		Vector2 scale = transform.lossyScale;
		p1 = transform.position - (Vector3.up * (box.size.y * scale.y / 2));
		p2 = p1 - (Vector3.up * 0.5f);

		RaycastHit2D hit = Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"));
		if(hit) {
			//transform.position = new Vector3(transform.position.x, hit.point.y + (box.size.y * scale.y), transform.position.z);
		}
		else {
			//Destroy(gameObject);
		}
	}

	void Update() {
		if(bounceTimer > 0) {
			bounceTimer -= Mathf.Min(Time.deltaTime, bounceTimer);
		}

		Debug.DrawLine(p1, p2, Color.red);
	}

	void OnTriggerEnter2D(Collider2D col) {
		Bounceable bounceable = col.gameObject.GetComponent<Bounceable>();

		if(bounceable && col.gameObject.rigidbody2D && bounceTimer == 0) {
			bounceForce = (bounceable.bounceForce != 0) ? bounceable.bounceForce : bounceForce;

			col.rigidbody2D.AddForce(new Vector2(horizontalForce, 0));
			float component = 0;
			
			col.gameObject.rigidbody2D.velocity = new Vector2(component, bounceForce);
			
			col.gameObject.SendMessage("Bounced", true);
			bounceTimer = 0.1f;
		}
	}
}