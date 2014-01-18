using UnityEngine;
using System.Collections;

public class RootbridgeGrowth : MonoBehaviour {

	private Vector3 test1;
	private Vector3 test2;

	void Start() {
		if(!safe()) {
			DestroyObject(gameObject);
		}
	}

	void Update() {
		Debug.DrawLine(test1, test2, Color.red);
	}

	bool safe() {
		Transform body = transform.Find("body");
		Vector3 extents = body.gameObject.GetComponent<BoxCollider2D>().size * .5f;

		Vector3 eye1 = transform.position;
		Vector3 eye2 = transform.position;
		extents.x *= transform.lossyScale.x;

		eye1.x += extents.x + .01f;
		eye2.x += extents.x + .02f;
		test1 = eye1;
		test2 = eye2;

		if(Physics2D.Linecast(eye1, eye2, 1 << LayerMask.NameToLayer("Ground"))) {
			return true;
		}
		else {
			return false;
		}
	}
}
