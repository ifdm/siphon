using UnityEngine;
using System;
using System.Collections;

public class Porter : MonoBehaviour {

	public GameObject next;
	public GameObject previous;
	public float delay = 1f;
	public enum Directions {UP, DOWN, LEFT, RIGHT};
	public Directions direction;

	// Next is true, previous is false
	[HideInInspector] public bool forward;
	[HideInInspector] public bool porting;

	private Porter nextScript;
	private Porter previousScript;

	private GameObject item;
	private float gracePeriod = 0.5f;

	void Start() {
		forward = true;

		nextScript = (next) ? next.GetComponent<Porter>() : null;
		previousScript = (previous) ? previous.GetComponent<Porter>() : null;
	}

	public void Port(GameObject other) {
		GameObject portal = (forward) ? next : previous;
		Porter script = (forward) ? nextScript : previousScript;
		FlyPath path = other.GetComponent<FlyPath>();
		float x, y;

		if(portal) {
			// Resolve position
			other.transform.position = portal.transform.position;
			// Resolve direction
			switch(script.direction) {
				case Directions.UP:
					x = 0;
					y = 2;
					break;
				case Directions.DOWN:
					x = 0;
					y = -2;
					break;
				case Directions.RIGHT:
					x = 2;
					y = 0;
					break;
				case Directions.LEFT:
				default:
					x = -2;
					y = 0;
					break;
			}
			// Set velocity
			path.velocity = new Vector2(x, y);
			// Decide next portal
			forward = script.forward = (forward) ? false : true;
			// Change kinematic back to normal
			Invoke("SetKinematic", gracePeriod);
		}
		else {
			Debug.Log("No portal attached. Not porting.");
		}
	}
	
	void SetKinematic() {
		if(item) {
			Debug.Log("Kinematic false");
			item.rigidbody2D.isKinematic = false;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		item = col.gameObject;
		if(col.gameObject.GetComponent<FlyPath>()) {
			Debug.Log("Kinematic true");
			item.rigidbody2D.isKinematic = true;
			Port(item);
		}
	}
}
