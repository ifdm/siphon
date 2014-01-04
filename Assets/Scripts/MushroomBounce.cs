using UnityEngine;
using System.Collections;

public class MushroomBounce : MonoBehaviour {

	public float firstBounce = 875f;
	public float secondBounce = 950f;

	private bool bounced = false;
	private float bounceTimer = 0;

	void Awake() {		
		RaycastHit2D hit = Physics2D.Linecast(transform.position, transform.position - (Vector3.up * 100), 1 << LayerMask.NameToLayer("Ground"));
		if(hit) {
			transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
		}
	}

	void Update() {
		if(bounceTimer > 0){bounceTimer -= Mathf.Min(Time.deltaTime, bounceTimer);}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.rigidbody2D && bounceTimer == 0) {
			float bounce;
			Bounceable script = col.gameObject.GetComponent<Bounceable>();

			if(script) {
				if(script.bounced) {
					bounce = secondBounce;
				}
				else {
					bounce = firstBounce;
				}
			}
			else {
				bounce = firstBounce;
			}

			if(col.gameObject.rigidbody2D.velocity.y < 0) {
				if(col.gameObject.rigidbody2D.velocity.y > -(bounce / 35)) {
					col.gameObject.rigidbody2D.velocity = new Vector2(col.gameObject.rigidbody2D.velocity.x, (bounce / 35));
				}
				else {
					col.gameObject.rigidbody2D.velocity = new Vector2(col.gameObject.rigidbody2D.velocity.x, -col.gameObject.rigidbody2D.velocity.y * 35);
				}

				col.gameObject.SendMessage("Bounce");
				bounceTimer = 0.1f;
			}
		}
	}
}