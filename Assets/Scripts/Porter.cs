using UnityEngine;
using System;
using System.Collections;

public class Porter : MonoBehaviour {

	public GameObject next;
	public GameObject previous;
	public float delay = 1f;
	public enum directions {Up, Down, Left, Right};
	public directions direction;

	// Next is true, previous is false
	[HideInInspector] public bool forward;
	[HideInInspector] public bool porting;

	private Porter nextScript;
	private Porter previousScript;

	private GameObject item;

	void Start() {
		forward = true;
		porting = false;

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
				case directions.Up:
					x = 0;
					y = 2;
					break;
				case directions.Down:
					x = 0;
					y = -2;
					break;
				case directions.Right:
					x = 2;
					y = 0;
					break;
				case directions.Left:
				default:
					x = -2;
					y = 0;
					break;
			}
			// Set velocity
			path.velocity = new Vector2(x, y);
			// Decide next portal
			forward = script.forward = (forward) ? false : true;
			// 
		}
		else {
			Debug.Log("No portal attached. Not porting.");
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.GetComponent<FlyPath>()) {
			col.gameObject.rigidbody2D.isKinematic = true;
			Port(col.gameObject);
		}
	}
}
