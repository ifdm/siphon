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

	void OnCollisionEnter2D(Collision2D col) {
		foreach (ContactPoint2D contact in col.contacts) {
			if(contact.collider.name.Equals("Player") && contact.normal.y < 0){
				Bounce(col.gameObject);
				break;
			}
		}
	}

	void Bounce(GameObject other) {
		if(other.rigidbody2D) {
			if(bounced) {
				other.rigidbody2D.AddForce(new Vector2 (0, secondBounce));
			}
			else {
				other.rigidbody2D.AddForce(new Vector2(0, firstBounce));
				bounced = true;
			}
		}
	}
}