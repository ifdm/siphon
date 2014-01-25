using UnityEngine;
using System.Collections;

public class RootbridgeGrowth : MonoBehaviour {

	private GameObject seed;
	private Transform body;

	void Start() {
		seed = GameObject.Find("Seed");
		body = transform.Find("body");

		if(!safe()) {
			DestroyObject(gameObject);
		}
	}

	bool safe() {
		float radius = seed.GetComponent<CircleCollider2D>().radius * seed.transform.lossyScale.x;

		Vector3 extents = body.gameObject.GetComponent<BoxCollider2D>().size * .5f;
		extents = Vector2.Scale(extents, transform.lossyScale);

		Vector3 p1 = transform.position;
		Vector3 p2 = transform.position;

		p1.x += 0.1f;
		p1.y += extents.y + 0.01f;

		p2.x += radius + 0.2f;
		p2.y += extents.y + 0.01f;

		// Right
		if(Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"))) {
			transform.position = new Vector3(transform.position.x - extents.x + radius, transform.position.y, transform.position.z);
			return !isGrounded(transform.position);
		}

		p1.x -= 0.2f + radius;
		p2.x -= 0.2f + radius;

		// Left
		if(Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"))) {
			transform.position = new Vector3(transform.position.x + extents.x - radius, transform.position.y, transform.position.z);
			return !isGrounded(transform.position);
		}

		return false;
	}

	bool isGrounded(Vector3 position) {
		float padding = 0.1f;
		float radius = seed.GetComponent<CircleCollider2D>().radius * seed.transform.lossyScale.x;
		Vector3 extents = body.gameObject.GetComponent<BoxCollider2D>().size;

		Vector3 p1 = position;
		Vector3 p2 = position;

		p1.x -= (extents.x + radius * 2) - padding;
		p1.y -= 0.2f;
		p2.x += (extents.x + radius * 2) - padding;
		p2.y -= 0.2f;

		// First Ground Check
		if(Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"))) {
			return true;
		}

		p1.y -= 0.2f;
		p2.y -= 0.2f;

		// Second Ground Check
		if(Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"))) {
			return true;
		}

		p1.y -= 0.2f;
		p2.y -= 0.2f;

		// Third Ground Check
		if(Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"))) {
			return true;
		}

		return false;
	}
}
