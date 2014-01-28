using UnityEngine;
using System.Collections;

public class RootbridgeGrowth : MonoBehaviour {

	private Transform body;
	private Transform oAnim;

	void Start() {
		body = transform.Find("body");
		oAnim = transform.Find("animation");

		if(!safe()) {
			DestroyObject(gameObject);
		}
	}

	bool safe() {
		GameObject seed = GameObject.Find("Seed");

		float radius = seed.GetComponent<CircleCollider2D>().radius * seed.transform.lossyScale.x;

		Vector3 extents = body.gameObject.GetComponent<BoxCollider2D>().size * .5f;
		extents = Vector2.Scale(extents, transform.lossyScale);

		Vector3 p1 = transform.position;
		Vector3 p2 = transform.position;

		p1.x += 0.1f;
		p1.y += extents.y + 0.01f;

		p2.x += radius + 0.1f;
		p2.y += extents.y + 0.01f;

		// Right
		if(Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"))) {
			transform.position = new Vector3(transform.position.x - extents.x + radius, transform.position.y, transform.position.z);
			oAnim.rotation = new Quaternion(0, -180, 0, 1);
			return !isGrounded(transform.position, radius);
		}

		p1.x -= 0.3f + radius;
		p2.x -= 0.3f + radius;

		// Left
		if(Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"))) {
			transform.position = new Vector3(transform.position.x + extents.x - radius, transform.position.y, transform.position.z);
			oAnim.position = new Vector3(oAnim.position.x + extents.x - radius * 2, oAnim.position.y, oAnim.position.z);
			oAnim.rotation = new Quaternion(0, 0, 0, 1);

			return !isGrounded(transform.position, radius);
		}

		return false;
	}

	bool isGrounded(Vector3 position, float radius) {
		float padding = 0.5f;
		float spacer = 0.3f;
		Vector3 extents = body.gameObject.GetComponent<BoxCollider2D>().size;

		Vector3 p1 = position;
		Vector3 p2 = position;

		p1.x -= (extents.x + radius * 2) - padding;
		p1.y -= spacer;
		p2.x += (extents.x + radius * 2) - padding;
		p2.y -= spacer;

		// First Bottom Ground Check
		if(Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"))) {
			return true;
		}

		p1.y -= spacer;
		p2.y -= spacer;

		// Second Bottom Ground Check
		if(Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"))) {
			return true;
		}

		p1.y -= spacer;
		p2.y -= spacer;

		// Third Bottom Ground Check
		if(Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"))) {
			return true;
		}

		p1.y += spacer * 4;
		p2.y += spacer * 4;

		// First Top Ground Check
		if(Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"))) {
			return true;
		}

		p1.y += spacer;
		p2.y += spacer;

		// Second Top Ground Check
		if(Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"))) {
			return true;
		}

		p1.y += spacer;
		p2.y += spacer;

		// Third Top Ground Check
		if(Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer("Ground"))) {
			return true;
		}

		return false;
	}
}
