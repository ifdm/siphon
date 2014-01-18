using UnityEngine;
using System.Collections;

public class MushroomBounce : MonoBehaviour {

	public float firstBounce = 20f;
	public float secondBounce = 24f;

	private float bounceTimer = 0;

	void Awake() {		
		RaycastHit2D hit = Physics2D.Linecast(transform.position, transform.position - (Vector3.up * 0.5f), 1 << LayerMask.NameToLayer("Ground"));
		if(hit) {
			transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
		}
		else {
			Destroy(gameObject);
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
				var component = col.gameObject.rigidbody2D.velocity.x + Input.GetAxis("Horizontal") * 2;

				if(Mathf.Abs(col.gameObject.rigidbody2D.velocity.y) < bounce) {
					col.gameObject.rigidbody2D.velocity = new Vector2(component, bounce);
				}
				else {
					col.gameObject.rigidbody2D.velocity = new Vector2(component, -col.gameObject.rigidbody2D.velocity.y);
				}

				col.gameObject.SendMessage("Bounce");
				bounceTimer = 0.1f;
			}
		}
	}
}