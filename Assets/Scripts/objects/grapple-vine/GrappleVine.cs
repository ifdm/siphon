using UnityEngine;
using System.Collections;

public class GrappleVine : MonoBehaviour {

	Vector3 startPoint;
	Vector3 endPoint;

	void Start () {
		startPoint = new Vector3(0, 2, 0);
		Vector3 up = transform.position;
		up.y += 1000;
		RaycastHit2D cast = Physics2D.Linecast(transform.position, up, 1 << LayerMask.NameToLayer("Ground"));
		endPoint = (Vector3)cast.point - transform.position;
		
		if(!cast || Vector3.Distance(startPoint, endPoint) > 100) {
			DestroyObject(gameObject);
		}

		EdgeCollider2D edge = GetComponent<EdgeCollider2D>();
		Vector2[] points = new Vector2[2];
		points[0] = (Vector2)(startPoint);
		points[1] = (Vector2)(endPoint);
		edge.points = points;
	}
	
	// Update is called once per frame
	/*void Update () {
		Debug.DrawLine(startPoint, endPoint, Color.red);
		if(climbing == null) {
			RaycastHit2D cast = Physics2D.Linecast(startPoint, endPoint, 1 << LayerMask.NameToLayer("Player"));
			if(cast) {
				PlayerMovement movement = cast.rigidbody.gameObject.GetComponent<PlayerMovement>();
				if(!dirty) {
					movement.state = PlayerState.Climbing;
					climbing = movement;
					dirty = true;
				}
			}
		}
		else {
			RaycastHit2D cast = Physics2D.Linecast(startPoint, endPoint, 1 << LayerMask.NameToLayer("Player"));
			PlayerMovement movement = climbing.gameObject.GetComponent<PlayerMovement>();
			if(!cast) {
				movement.state = PlayerState.Jumping;
				climbing = null;
				dirty = false;
			}
			else if(movement.gameObject.rigidbody2D.velocity.y < 0) {
				dirty = false;
				climbing = null;
			}
		}
	}*/

	void Update() {
		Debug.DrawLine(startPoint, endPoint, Color.red);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			PlayerMovement player = col.gameObject.GetComponent<PlayerMovement>();
			player.ChangeState(PlayerState.Climbing);
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			PlayerMovement player = col.gameObject.GetComponent<PlayerMovement>();
			player.ChangeState(PlayerState.Falling);
		}
	}
}