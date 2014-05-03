using UnityEngine;
using System.Collections;

public class GiantBoulder : MonoBehaviour {

	void OnCollisionStay2D(Collision2D col) {
		if(col.gameObject.name == "Player") {
			if(rigidbody2D.isKinematic){rigidbody2D.isKinematic = false;}
			float x = Mathf.Max(Mathf.Abs(rigidbody2D.velocity.x), Mathf.Abs(col.gameObject.rigidbody2D.velocity.x)) * Mathf.Sign(col.gameObject.rigidbody2D.velocity.x) * .8f + (Random.value * .2f);
			if(col.gameObject.GetComponent<PlayerControl>().state == PlayerState.Idling) {
				x /= 2;
			}
			if(GameObject.Find("Main Camera").GetComponent<CameraFollow>().shake > 0) {
				x /= 3.5f;
			}
			col.gameObject.rigidbody2D.velocity = new Vector2(x, col.gameObject.rigidbody2D.velocity.y);
		}
	}
}
