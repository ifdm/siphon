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
	[HideInInspector] public bool forward = true;
	[HideInInspector] public bool ready = true;

	private Porter nextScript;
	private Porter previousScript;

	private GameObject item;

	void Start() {
		ready = true;
		nextScript = (next) ? next.GetComponent<Porter>() : null;
		previousScript = (previous) ? previous.GetComponent<Porter>() : null;
	}
	
	void Update() {
		if(ready && item) {
			Send(item);
		}
	}

	private void Send(GameObject other) {
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
			path.velocity = new Vector2(x, y);
			// Resolve hidden rendering
			other.renderer.enabled = true;
			// Tell the next portal, "You are not prepared."
			script.ready = false;
			// Decide next portal
			forward = script.forward = (forward) ? false : true;
		}
		else {
			Debug.Log("No portal attached. Not porting.");
		}
	}

	public void Port(GameObject other) {
		if(ready) {
			Debug.Log("Sending");
			Send(other);
		}
		else {
			Debug.Log("Not sending");
			FlyPath path = other.GetComponent<FlyPath>();
			// Stop the object
			path.velocity = new Vector2(0, 0);
			// Hide the object
			//other.renderer.enabled = false;
			// Keep a copy
			item = other;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		Debug.Log(col.gameObject.name);
		Debug.Log(ready);
	}

	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.name == "Fly") {
			ready = true;
		}
	}
}
