using UnityEngine;
using System;
using System.Collections;

public class Porter : MonoBehaviour {

	public GameObject next;
	public GameObject previous;
	
	public float delay = 1f;
	public float gracePeriod = 0.5f;
	
	public enum Directions {UP, DOWN, LEFT, RIGHT};
	public Directions direction;

	// Next is true, previous is false
	[HideInInspector] public bool forward;

	private Porter nextScript;
	private Porter previousScript;

	void Start() {
		forward = true;
		nextScript = (next) ? next.GetComponent<Porter>() : null;
		previousScript = (previous) ? previous.GetComponent<Porter>() : null;
	}

	public void Port(GameObject other) {
		GameObject portal = (forward) ? next : previous;
		Porter script = (forward) ? nextScript : previousScript;
		FlyPath path = other.GetComponent<FlyPath>();
		float x, y, current;

		if(portal) {
			// Resolve position
			other.transform.position = portal.transform.position;
			// Resolve current velocity
			current = Mathf.Abs(path.velocity.x) > Mathf.Abs(path.velocity.y) ? Mathf.Abs(path.velocity.x) : Mathf.Abs(path.velocity.y);
			Debug.Log(current);
			// Resolve direction
			switch(script.direction) {
				case Directions.UP:
					x = 0;
					y = current;
					break;
				case Directions.DOWN:
					x = 0;
					y = -current;
					break;
				case Directions.RIGHT:
					x = current;
					y = 0;
					break;
				case Directions.LEFT:
				default:
					x = -current;
					y = 0;
					break;
			}
			// Stop object anticipating a delay
			path.velocity = new Vector2(0, 0);
			// Set velocity
			StartCoroutine(SetVelocity(path, x, y, delay));
			// Change kinematic back to normal
			StartCoroutine(SetKinematic(other, gracePeriod + delay));
			// Decide next portal
			forward = script.forward = (forward) ? false : true;
		}
		else {
			Debug.Log("No portal attached. Not porting.");
		}
	}
	
	IEnumerator SetVelocity(FlyPath path, float x, float y, float delay) {
		yield return new WaitForSeconds(delay);
		path.velocity = new Vector2(x, y);
	}
	
	IEnumerator SetKinematic(GameObject other, float delay) {
		yield return new WaitForSeconds(delay);
		other.rigidbody2D.isKinematic = false;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.GetComponent<FlyPath>()) {
			col.gameObject.rigidbody2D.isKinematic = true;
			Port(col.gameObject);
		}
	}
}
