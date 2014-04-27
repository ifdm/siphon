using UnityEngine;
using System.Collections;

public class GiantBoulder : MonoBehaviour {

	void OnCollisionStay2D(Collision2D col) {
		if(col.gameObject.name == "Player") {
			if(rigidbody2D.isKinematic){rigidbody2D.isKinematic = false;}
			float x = Mathf.Max(Mathf.Abs(rigidbody2D.velocity.x), Mathf.Abs(col.gameObject.rigidbody2D.velocity.x)) * Mathf.Sign(col.gameObject.rigidbody2D.velocity.x) * .9f + (Random.value * .1f);
			col.gameObject.rigidbody2D.velocity = new Vector2(x, col.gameObject.rigidbody2D.velocity.y);

			//col.gameObject.rigidbody2D.AddForce(new Vector2(10, 0));
		}
	}
}
